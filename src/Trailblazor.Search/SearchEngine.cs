using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search;

internal sealed class SearchEngine(
    ISearchEngineOptionsProvider _searchEngineOptionsProvider,
    IServiceProvider _serviceProvider) : ISearchEngine
{
    public Task<IConcurrentSearchOperationCallback> SendRequestAsync<TRequest>(string operationKey, TRequest request, CancellationToken cancellationToken)
        where TRequest : class, ISearchRequest
    {
        var searchOperationConfiguration = _searchEngineOptionsProvider.GetOptions().GetOperationConfigurationByKey(operationKey) ?? throw new ArgumentException($"No search operation for key '{operationKey}' has been configured.");
        var operationPipeline = _serviceProvider.GetRequiredKeyedService<ISearchRequestPipeline<TRequest>>(operationKey);

        return operationPipeline.RunPipelineForOperationAsync(searchOperationConfiguration, request, cancellationToken);
    }
}
