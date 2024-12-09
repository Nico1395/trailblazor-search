namespace Trailblazor.Search;

public interface ISearchRequestHandler<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task HandleAsync(SearchRequestHandlerContext<TRequest> context, IConcurrentSearchRequestCallback callback, CancellationToken cancellationToken);
}
