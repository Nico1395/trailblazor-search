using Trailblazor.Search.Requests;

namespace Trailblazor.Search;

/// <summary>
/// Service sends <see cref="ISearchRequest"/> into <see cref="ISearchRequestPipeline{TRequest}"/>.
/// </summary>
public interface ISearchEngine
{
    /// <summary>
    /// Sends the search <paramref name="request"/> of type <typeparamref name="TRequest"/> into the configured pipeline for the operation with the given <paramref name="operationKey"/>.
    /// </summary>
    /// <typeparam name="TRequest">Type of request to be sent.</typeparam>
    /// <param name="operationKey">Key of the configured search operation.</param>
    /// <param name="request">Search request to be sent.</param>
    /// <param name="cancellationToken">Cancellation token for aborting the operation.</param>
    /// <returns>An <see cref="IConcurrentSearchOperationCallback"/> that can be subscribed to, to get notified about the changing <see cref="IConcurrentSearchResponse"/>s.</returns>
    public Task<IConcurrentSearchOperationCallback> SendRequestAsync<TRequest>(string operationKey, TRequest request, CancellationToken cancellationToken)
        where TRequest : class, ISearchRequest;
}
