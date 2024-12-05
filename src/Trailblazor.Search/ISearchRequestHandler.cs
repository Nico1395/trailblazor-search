namespace Trailblazor.Search;

public interface ISearchRequestHandler<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task HandleAsync(SearchRequestHandlerContext context, IAsyncSearchRequestCallback callback, CancellationToken cancellationToken);
}
