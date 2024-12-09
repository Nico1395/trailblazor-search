namespace Trailblazor.Search;

/// <summary>
/// Thread-safe callback for <see cref="ISearchRequestHandler{TRequest}"/> to report back their results and state for a specific search operation.
/// </summary>
/// <remarks>
/// This callback can be subscribed to by the sender of the <see cref="ISearchRequest"/> to get notified about when the <see cref="IConcurrentSearchResponse"/> changes.
/// </remarks>
public interface IConcurrentSearchOperationCallback
{
    /// <summary>
    /// Subscribes to when the <see cref="IConcurrentSearchResponse"/> changes.
    /// </summary>
    /// <param name="eventHandler">Handler handling the search response changing.</param>
    public void Subscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler);

    /// <summary>
    /// Unsubscribes from when the <see cref="IConcurrentSearchResponse"/> changes.
    /// </summary>
    /// <param name="eventHandler">Handler handling the search response changing.</param>
    public void Unsubscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler);

    /// <summary>
    /// Gets the current search response.
    /// </summary>
    /// <returns>The current <see cref="IConcurrentSearchResponse"/>.</returns>
    public IConcurrentSearchResponse GetCurrentResponse();

    /// <summary>
    /// Reports back <see cref="ISearchResult"/>s and adds them to the <see cref="IConcurrentSearchResponse"/>.
    /// </summary>
    /// <param name="handlerMetadata"><see cref="SearchRequestHandlerMetadata"/> of the <see cref="ISearchRequestHandler{TRequest}"/>.</param>
    /// <param name="results">Search results to be added.</param>
    /// <param name="finished">Determines whether to report as finished as well or not. Otherwise <see cref="ReportFinishedAsync(Guid)"/> should be used to report the handler as completed.</param>
    public Task ReportResultsAsync(SearchRequestHandlerMetadata handlerMetadata, IEnumerable<ISearchResult> results, bool finished = false);

    /// <summary>
    /// Reports back as finished.
    /// </summary>
    /// <param name="handlerMetadata"><see cref="SearchRequestHandlerMetadata"/> of the <see cref="ISearchRequestHandler{TRequest}"/>.</param>
    public Task ReportFinishedAsync(SearchRequestHandlerMetadata handlerMetadata);

    /// <summary>
    /// Reports back that an error occurred.
    /// </summary>
    /// <param name="handlerMetadata"><see cref="SearchRequestHandlerMetadata"/> of the <see cref="ISearchRequestHandler{TRequest}"/>.</param>
    /// <param name="exception">Occurred excetion.</param>
    public Task ReportFailedAsync(SearchRequestHandlerMetadata handlerMetadata, Exception? exception = null);
}
