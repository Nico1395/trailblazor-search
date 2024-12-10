using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search;

public interface ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task<IConcurrentSearchOperationCallback> RunPipelineForOperationAsync(ISearchOperationConfiguration searchOperationConfiguration, TRequest request, CancellationToken cancellationToken);
}
