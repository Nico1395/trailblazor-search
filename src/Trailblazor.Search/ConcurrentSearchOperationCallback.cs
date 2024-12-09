using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

/// <inheritdoc/>
public sealed class ConcurrentSearchOperationCallback(List<Guid> _handlerIds, ICallbackLoggingHandler _loggingHandler) : IConcurrentSearchOperationCallback
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

    /// <inheritdoc/>
    public void Subscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler)
    {
        OnSearchResponseChanged += eventHandler;
        InvokeOnSearchResultChanged();
    }

    /// <inheritdoc/>
    public void Unsubscribe(EventHandler<SearchResponseChangedEventArgs> eventHandler)
    {
        OnSearchResponseChanged -= eventHandler;
    }

    /// <inheritdoc/>
    public IConcurrentSearchResponse GetCurrentResponse()
    {
        return _response;
    }

    /// <inheritdoc/>
    public async Task ReportResultsAsync(SearchRequestHandlerMetadata handlerMetadata, IEnumerable<ISearchResult> results, bool finished = false)
    {
        var resultsList = results.ToList();
        _response.AddResults(resultsList);

        if (finished)
        {
            await ReportFinishedAsync(handlerMetadata);
            _loggingHandler.LogReportResults(handlerMetadata, resultsList);
        }
        else
        {
            InvokeOnSearchResultChanged();
            _loggingHandler.LogReportResults(handlerMetadata, resultsList);
        }
    }

    /// <inheritdoc/>
    public Task ReportFinishedAsync(SearchRequestHandlerMetadata handlerMetadata)
    {
        _response.State.SetHandlerState(handlerMetadata.HandlerId, SearchRequestHandlerState.Finished);
        InvokeOnSearchResultChanged();

        _loggingHandler.LogReportFinished(handlerMetadata);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task ReportFailedAsync(SearchRequestHandlerMetadata handlerMetadata, Exception? exception = null)
    {
        _response.State.SetHandlerState(handlerMetadata.HandlerId, SearchRequestHandlerState.Failed);
        InvokeOnSearchResultChanged();

        _loggingHandler.LogReportFailed(handlerMetadata, exception);
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
