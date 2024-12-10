namespace Trailblazor.Search;

/// <summary>
/// Handler that is invoked in a search operation for the given <typeparamref name="TRequest"/>.
/// </summary>
/// <typeparam name="TRequest">Type of search request to be handled.</typeparam>
public interface ISearchRequestHandler<TRequest>
    where TRequest : class, ISearchRequest
{
    /// <summary>
    /// Handles the <typeparamref name="TRequest"/> contained in the given <paramref name="context"/>.
    /// </summary>
    /// <param name="context">Context of the search handler for the current search operation.</param>
    /// <param name="callback">Callback that can be used to report back to the operation.</param>
    /// <param name="cancellationToken">Cancellation for aborting the operation.</param>
    public Task HandleAsync(SearchRequestHandlerContext<TRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken);
}
