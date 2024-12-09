using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.DependencyInjection;
using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

public sealed class DefaultSearchRequestPipeline<TRequest>(
    IServiceProvider _serviceProvider,
    ICallbackLoggingHandler _pipelineLoggingHandler) : ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task<IConcurrentSearchOperationCallback> RunPipelineForOperationAsync(ISearchOperationConfiguration searchOperationConfiguration, TRequest request, CancellationToken cancellationToken)
    {
        var handlerContextPairings = _serviceProvider.GetKeyedServices<ISearchRequestHandler<TRequest>>(searchOperationConfiguration.Key).Select(handler => 
        {
            var context = new SearchRequestHandlerContext<TRequest>()
            {
                Request = request,
                Metadata = new()
                {
                    HandlerId = Guid.NewGuid(),
                    HandlerType = handler.GetType(),
                }
            };

            return (Context: context, Handler: handler);
        }).ToList();

        var callback = new ConcurrentSearchOperationCallback(
            handlerContextPairings.Select(p => p.Context.Metadata.HandlerId).ToList(),
            _pipelineLoggingHandler);

        foreach (var threadConfiguration in searchOperationConfiguration.ThreadConfigurations.OrderBy(t => t.Priority))
        {
            var pairs = handlerContextPairings.Where(p => threadConfiguration.RequestHandlerTypes.Contains(p.Context.Metadata.HandlerType)).ToList();
            _ = OpenThreadAsync(callback, pairs, cancellationToken);
        }

        return Task.FromResult<IConcurrentSearchOperationCallback>(callback);
    }

    private async Task OpenThreadAsync(IConcurrentSearchOperationCallback callback, List<(SearchRequestHandlerContext<TRequest> Context, ISearchRequestHandler<TRequest> Handler)> handlerContextPairings, CancellationToken cancellationToken)
    {
        foreach (var (HandlerContext, Handler) in handlerContextPairings)
            await Handler.HandleAsync(HandlerContext, callback, cancellationToken);
    }
}
