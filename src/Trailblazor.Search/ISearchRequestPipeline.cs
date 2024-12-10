using Trailblazor.Search.DependencyInjection;
using Trailblazor.Search.Requests;

namespace Trailblazor.Search;

public interface ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task<IConcurrentSearchOperationCallback> RunPipelineForOperationAsync(ISearchOperationConfiguration searchOperationConfiguration, TRequest request, CancellationToken cancellationToken);
}
