using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

public class DefaultSearchRequestPipeline<TRequest>(IServiceProvider serviceProvider, ICallbackLoggingHandler pipelineLoggingHandler) : ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;
    protected ICallbackLoggingHandler PipelineLoggingHandler { get; } = pipelineLoggingHandler;

    public Task<IAsyncSearchRequestCallback> OpenPipelineAsync(TRequest request, CancellationToken cancellationToken)
    {
        var handlerContextPairings =  ServiceProvider.GetServices<ISearchRequestHandler<TRequest>>().Select(handler => 
        {
            var context = new SearchRequestHandlerContext()
            {
                HandlerId = Guid.NewGuid(),
                Request = request,
            };

            return (Context: context, Handler: handler);
        });

        var callback = new AsyncSearchRequestCallback(
            handlerContextPairings.Select(p => p.Context.HandlerId).ToList(),
            PipelineLoggingHandler);

        foreach (var (HandlerContext, Handler) in handlerContextPairings)
            _ = Handler.HandleAsync(HandlerContext, callback, cancellationToken);

        return Task.FromResult<IAsyncSearchRequestCallback>(callback);
    }
}
