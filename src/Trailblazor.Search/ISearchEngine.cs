namespace Trailblazor.Search;

public interface ISearchEngine
{
    public Task<IConcurrentSearchRequestCallback> SendRequestAsync<TRequest>(string operationKey, TRequest request, CancellationToken cancellationToken)
        where TRequest : class, ISearchRequest;
}
