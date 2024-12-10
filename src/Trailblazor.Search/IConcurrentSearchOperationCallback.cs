using Trailblazor.Search.DependencyInjection;

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
    /// Allows a pipeline to initialize the callback.
    /// </summary>
    /// <param name="operationConfiguration">Configuration of the search operation.</param>
    /// <param name="searchRequest">Most current search request for the operation.</param>
    public void Connect(ISearchOperationConfiguration operationConfiguration, ISearchRequest searchRequest);

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
    /// <param name="handerId">ID of the handler reporting back.</param>
    /// <param name="results">Search results to be added.</param>
    /// <param name="finished">Determines whether to report as finished as well or not. Otherwise <see cref="ReportFinishedAsync(Guid)"/> should be used to report the handler as completed.</param>
    public Task ReportResultsAsync(Guid handerId, IEnumerable<ISearchResult> results, bool finished = false);

    /// <summary>
    /// Reports back as finished.
    /// </summary>
    /// <param name="handerId">ID of the handler reporting back.</param>
    public Task ReportFinishedAsync(Guid handerId);

    /// <summary>
    /// Reports back that an error occurred.
    /// </summary>
    /// <param name="handerId">ID of the handler reporting back.</param>
    /// <param name="exception">Occurred excetion.</param>
    public Task ReportFailedAsync(Guid handerId, Exception? exception = null);
}
