using Microsoft.Extensions.DependencyInjection;

namespace Trailblazor.Search;

internal sealed class SearchEngine(IServiceProvider _serviceProvider) : ISearchEngine
{
    public Task<IConcurrentSearchRequestCallback> SendRequestAsync<TRequest>(string operationKey, TRequest request, CancellationToken cancellationToken)
        where TRequest : class, ISearchRequest
    {
        var operationPipeline = _serviceProvider.GetRequiredKeyedService<ISearchRequestPipeline<TRequest>>(operationKey);
        return operationPipeline.RunPipelineForOperationAsync(operationKey, request, cancellationToken);
    }
}
