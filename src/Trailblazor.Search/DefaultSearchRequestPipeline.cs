using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.DependencyInjection;
using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

public class DefaultSearchRequestPipeline<TRequest>(
    IServiceProvider serviceProvider,
    ISearchEngineOptionsProvider searchEngineOptionsProvider,
    ICallbackLoggingHandler pipelineLoggingHandler) : ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;
    protected ISearchEngineOptionsProvider SearchEngineOptionsProvider { get; } = searchEngineOptionsProvider;
    protected ICallbackLoggingHandler PipelineLoggingHandler { get; } = pipelineLoggingHandler;

    public Task<IConcurrentSearchRequestCallback> RunPipelineForOperationAsync(string operationKey, TRequest request, CancellationToken cancellationToken)
    {
        var operationConfiguration = SearchEngineOptionsProvider.GetOptions().OperationConfigurations.SingleOrDefault(o => o.Key == operationKey) ?? throw new InvalidOperationException($"No search operation with key '{operationKey}' has been registered.");
        var handlerContextPairings =  ServiceProvider.GetKeyedServices<ISearchRequestHandler<TRequest>>(operationKey).Select(handler => 
        {
            var context = new SearchRequestHandlerContext<TRequest>()
            {
                HandlerId = Guid.NewGuid(),
                Request = request,
            };

            return (Context: context, Handler: handler, HandlerType: handler.GetType());
        }).ToList();

        var callback = new ConcurrentSearchRequestCallback(
            handlerContextPairings.Select(p => p.Context.HandlerId).ToList(),
            PipelineLoggingHandler);

        foreach (var threadConfiguration in operationConfiguration.ThreadConfigurations.OrderBy(t => t.Priority))
        {
            var pairs = handlerContextPairings
                .Where(p => threadConfiguration.RequestHandlerTypes.Contains(p.HandlerType))
                .Select(p => (p.Context, p.Handler))
                .ToList();

            _ = OpenThreadAsync(callback, pairs, cancellationToken);
        }

        return Task.FromResult<IConcurrentSearchRequestCallback>(callback);
    }

    private async Task OpenThreadAsync(IConcurrentSearchRequestCallback callback, List<(SearchRequestHandlerContext<TRequest> Context, ISearchRequestHandler<TRequest> Handler)> handlerContextPairings, CancellationToken cancellationToken)
    {
        foreach (var (HandlerContext, Handler) in handlerContextPairings)
            await Handler.HandleAsync(HandlerContext, callback, cancellationToken);
    }
}
