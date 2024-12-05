using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

internal sealed class AsyncSearchRequestCallback(List<Guid> _handlerIds, ICallbackLoggingHandler _loggingHandler) : IAsyncSearchRequestCallback
{
    private readonly SearchRequestProgress _progress = new(_handlerIds);
    private readonly ISearchResponse _response = new SearchResponse();

    public event EventHandler<SearchRequestChangedEventArgs>? OnSearchRequestChanged;

    public Task ReportResultsAsync(Guid handlerId, IEnumerable<ISearchResult> results)
    {
        var resultsList = results.ToList();

        // TODO -> Combine unique results

        var args = new SearchRequestChangedEventArgs()
        {
            Response = _response,
            Progress = _progress,
        };
        OnSearchRequestChanged?.Invoke(this, args);

        _loggingHandler.LogReportResults(handlerId, resultsList);
        return Task.CompletedTask;
    }

    public Task ReportFinishedAsync(Guid handlerId)
    {
        _progress.SetHandlerState(handlerId, SearchRequestHandlerState.Finished);
        _loggingHandler.LogReportFinished(handlerId);

        return Task.CompletedTask;
    }
    
    public Task ReportFailedAsync(Guid handlerId, Exception? exception = null)
    {
        _progress.SetHandlerState(handlerId, SearchRequestHandlerState.Failed);
        _loggingHandler.LogReportFailed(handlerId, exception);

        return Task.CompletedTask;
    }
}
