using Trailblazor.Search.DependencyInjection;
using Trailblazor.Search.Logging;

namespace Trailblazor.Search;

/// <inheritdoc/>
public sealed class ConcurrentSearchOperationCallback(ICallbackLoggingHandler _loggingHandler) : IConcurrentSearchOperationCallback
{
    private ISearchRequest? _searchRequest;
    private ConcurrentSearchResponse _response = new([]);
    private ISearchOperationConfiguration? _operationConfiguration;

    private readonly Lock _eventLock = new();
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
    public void Connect(ISearchOperationConfiguration operationConfiguration, ISearchRequest searchRequest)
    {
        _operationConfiguration = operationConfiguration;
        _searchRequest = searchRequest;

        var handlerIds = _operationConfiguration.ThreadConfigurations.SelectMany(t => t.HandlerConfigurations).Select(h => h.HandlerId).ToList();
        _response = new ConcurrentSearchResponse(handlerIds);

        InvokeOnSearchResultChanged();
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
    public async Task ReportResultsAsync(Guid handerId, IEnumerable<ISearchResult> results, bool finished = false)
    {
        var resultsList = results.ToList();
        _response.AddResults(resultsList);

        if (finished)
        {
            await ReportFinishedAsync(handerId);
            _loggingHandler.LogReportResults(handerId, resultsList);
        }
        else
        {
            InvokeOnSearchResultChanged();
            _loggingHandler.LogReportResults(handerId, resultsList);
        }
    }

    /// <inheritdoc/>
    public Task ReportFinishedAsync(Guid handerId)
    {
        _response.State.SetHandlerState(handerId, SearchRequestHandlerState.Finished);
        InvokeOnSearchResultChanged();

        _loggingHandler.LogReportFinished(handerId);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task ReportFailedAsync(Guid handerId, Exception? exception = null)
    {
        _response.State.SetHandlerState(handerId, SearchRequestHandlerState.Failed);
        InvokeOnSearchResultChanged();

        _loggingHandler.LogReportFailed(handerId, exception);
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
