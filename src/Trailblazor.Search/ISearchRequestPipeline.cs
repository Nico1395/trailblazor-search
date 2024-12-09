namespace Trailblazor.Search;

public interface ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task<IConcurrentSearchRequestCallback> RunPipelineForOperationAsync(string operationKey, TRequest request, CancellationToken cancellationToken);
}
