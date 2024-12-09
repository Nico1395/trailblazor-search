using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

internal sealed class ConcurrentSearchRequestCallback(List<Guid> _handlerIds, ICallbackLoggingHandler _loggingHandler) : IConcurrentSearchRequestCallback
{
    private readonly Lock _eventLock = new();

    private readonly ConcurrentSearchResponse _response = new(_handlerIds);

    private EventHandler<SearchResponseChangedEventArgs>? _onSearchResponseChanged;
    private event EventHandler<SearchResponseChangedEventArgs> OnSearchResponseChanged
    {
        add
        {
            lock (_eventLock)
                _onSearchResponseChanged += value;
        }
        remove
        {
            lock (_eventLock)
                _onSearchResponseChanged -= value;
        }
    }

    public void Subscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler)
    {
        OnSearchResponseChanged += eventHandler;
        InvokeOnSearchResultChanged();
    }

    public void Unsubscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler)
    {
        OnSearchResponseChanged -= eventHandler;
    }

    public IConcurrentSearchResponse GetCurrentResponse()
    {
        return _response;
    }

    public async Task ReportResultsAsync(Guid handlerId, IEnumerable<ISearchResult> results, bool finished = false)
    {
        var resultsList = results.ToList();
        _response.AddResults(resultsList);

        if (finished)
        {
            await ReportFinishedAsync(handlerId);
            _loggingHandler.LogReportResults(handlerId, resultsList);
        }
        else
        {
            InvokeOnSearchResultChanged();
            _loggingHandler.LogReportResults(handlerId, resultsList);
        }
    }

    public Task ReportFinishedAsync(Guid handlerId)
    {
        _response.State.SetHandlerState(handlerId, SearchRequestHandlerState.Finished);
        InvokeOnSearchResultChanged();

        _loggingHandler.LogReportFinished(handlerId);
        return Task.CompletedTask;
    }
    
    public Task ReportFailedAsync(Guid handlerId, Exception? exception = null)
    {
        _response.State.SetHandlerState(handlerId, SearchRequestHandlerState.Failed);
        InvokeOnSearchResultChanged();

        _loggingHandler.LogReportFailed(handlerId, exception);
        return Task.CompletedTask;
    }

    private void InvokeOnSearchResultChanged()
    {
        EventHandler<SearchResponseChangedEventArgs>? onSearchResponseChangedEventHandlers;
        lock (_eventLock)
            onSearchResponseChangedEventHandlers = _onSearchResponseChanged;

        onSearchResponseChangedEventHandlers?.Invoke(this, new SearchResponseChangedEventArgs(_response));
    }
}
