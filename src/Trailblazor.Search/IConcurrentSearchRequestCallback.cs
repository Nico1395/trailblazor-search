namespace Trailblazor.Search;

public interface IConcurrentSearchRequestCallback
{
    public void Subscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler);
    public void Unsubscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler);
    public IConcurrentSearchResponse GetCurrentResponse();
    public Task ReportResultsAsync(Guid handlerId, IEnumerable<ISearchResult> results, bool finished = false);
    public Task ReportFinishedAsync(Guid handlerId);
    public Task ReportFailedAsync(Guid handlerId, Exception? exception = null);
}
