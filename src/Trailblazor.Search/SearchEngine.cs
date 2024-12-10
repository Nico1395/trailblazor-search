using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.DependencyInjection;
using Trailblazor.Search.Requests;

namespace Trailblazor.Search;

internal sealed class SearchEngine(
    ISearchEngineOptionsProvider _searchEngineOptionsProvider,
    IServiceProvider _serviceProvider) : ISearchEngine
{
    public Task<IConcurrentSearchOperationCallback> SendRequestAsync<TRequest>(string operationKey, TRequest request, CancellationToken cancellationToken)
        where TRequest : class, ISearchRequest
    {
        var operationConfiguration = _searchEngineOptionsProvider.GetOptions().GetOperationConfigurationByKey(operationKey);
        ArgumentNullException.ThrowIfNull(operationConfiguration, nameof(operationKey));

        var operationPipeline = _serviceProvider.GetRequiredKeyedService<ISearchRequestPipeline<TRequest>>(operationKey);
        return operationPipeline.RunPipelineForOperationAsync(operationConfiguration, request, cancellationToken);
    }
}
