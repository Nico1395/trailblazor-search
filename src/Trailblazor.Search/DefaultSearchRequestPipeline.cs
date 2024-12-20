using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search;

/// <inheritdoc/>
public sealed class DefaultSearchRequestPipeline<TRequest>(IServiceProvider _serviceProvider) : ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    private sealed record HandlerContextPair(
        ISearchRequestHandler<TRequest> Handler,
        SearchRequestHandlerContext<TRequest> Context);

    /// <inheritdoc/>
    public Task<IConcurrentSearchOperationCallback> RunPipelineForOperationAsync(ISearchOperationConfiguration searchOperationConfiguration, TRequest request, CancellationToken cancellationToken)
    {
        var callback = _serviceProvider.GetRequiredKeyedService<IConcurrentSearchOperationCallback>(searchOperationConfiguration.Key);
        callback.Connect(searchOperationConfiguration, request);

        OpenThreads(
            searchOperationConfiguration,
            AggregateHandlerContexts(searchOperationConfiguration, request),
            callback,
            cancellationToken);

        return Task.FromResult(callback);
    }

    private List<HandlerContextPair> AggregateHandlerContexts(ISearchOperationConfiguration searchOperationConfiguration, TRequest request)
    {
        var handlerConfigurations = searchOperationConfiguration.ThreadConfigurations.SelectMany(t => t.HandlerConfigurations).ToList();
        List<HandlerContextPair> handlerContextPairs = [];

        foreach (var handler in _serviceProvider.GetKeyedServices<ISearchRequestHandler<TRequest>>(searchOperationConfiguration.Key))
        {
            var handlerConfiguration = handlerConfigurations.SingleOrDefault(h => h.HandlerType == handler.GetType());
            if (handlerConfiguration == null)
                continue;

            var context = new SearchRequestHandlerContext<TRequest>()
            {
                Request = request,
                HandlerConfiguration = handlerConfiguration,
            };

            handlerContextPairs.Add(new(handler, context));
        }

        return handlerContextPairs;
    }

    private void OpenThreads(ISearchOperationConfiguration searchOperationConfiguration, List<HandlerContextPair> pairs, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken)
    {
        foreach (var threadConfiguration in searchOperationConfiguration.ThreadConfigurations)
        {
            var targetHandlerContextPairs = pairs.Where(p => threadConfiguration.HandlerConfigurations.Contains(p.Context.HandlerConfiguration)).ToList();
            _ = HandleThreadAsync(callback, targetHandlerContextPairs, cancellationToken);
        }
    }

    private async Task HandleThreadAsync(IConcurrentSearchOperationCallback callback, List<HandlerContextPair> handlerContextPairs, CancellationToken cancellationToken)
    {
        foreach (var (handler, context) in handlerContextPairs.OrderBy(p => p.Context.HandlerConfiguration.Priority))
            await handler.HandleAsync(context, callback, cancellationToken);
    }
}
