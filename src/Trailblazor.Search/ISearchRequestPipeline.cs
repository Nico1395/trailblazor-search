namespace Trailblazor.Search;

public interface ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task<IAsyncSearchRequestCallback> OpenPipelineAsync(TRequest request, CancellationToken cancellationToken);
}
