namespace Trailblazor.Search;

public interface IAsyncSearchRequestCallback
{
    public event EventHandler<SearchRequestChangedEventArgs>? OnSearchRequestChanged;
    public Task ReportResultsAsync(Guid handlerId, IEnumerable<ISearchResult> results);
    public Task ReportFinishedAsync(Guid handlerId);
    public Task ReportFailedAsync(Guid handlerId, Exception? exception = null);
}
