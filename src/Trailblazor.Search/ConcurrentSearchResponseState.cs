namespace Trailblazor.Search;

/// <inheritdoc/>
public sealed class ConcurrentSearchResponseState(List<Guid> handlerIds) : IConcurrentSearchResponseState
{
    private readonly Lock _handlerStatesLock = new();
    private readonly Dictionary<Guid, SearchRequestHandlerState> _lockedHandlerStates = handlerIds
        .Select(h => new KeyValuePair<Guid, SearchRequestHandlerState>(h, SearchRequestHandlerState.Pending))
        .ToDictionary();

    /// <inheritdoc/>
    public IReadOnlyDictionary<Guid, SearchRequestHandlerState> HandlerStates
    {
        get
        {
            lock (_handlerStatesLock)
                return _lockedHandlerStates;
        }
    }

    /// <inheritdoc/>
    public uint GetFinishedHandlerCount()
    {
        return GetCompletedHandlerCounts().Finished;
    }

    /// <inheritdoc/>
    public uint GetFailedHandlerCount()
    {
        return GetCompletedHandlerCounts().Failed;
    }

    /// <inheritdoc/>
    public uint GetCompletedHandlerCount()
    {
        return GetCompletedHandlerCounts().Total;
    }

    /// <inheritdoc/>
    public uint GetPendingHandlerCount()
    {
        return GetCompletedHandlerCounts().Pending;
    }

    /// <inheritdoc/>
    public double GetCompletedHandlersPercentage()
    {
        var completedHandlerCount = GetCompletedHandlerCount();
        lock (_handlerStatesLock)
            return completedHandlerCount / _lockedHandlerStates.Count;
    }

    /// <inheritdoc/>
    public bool IsCompleted()
    {
        return GetCompletedHandlersPercentage() == 1;
    }

    /// <inheritdoc/>
    public void SetHandlerState(Guid handlerId, SearchRequestHandlerState handlerState)
    {
        lock (_handlerStatesLock)
            _lockedHandlerStates[handlerId] = handlerState;
    }

    private (uint Finished, uint Failed, uint Pending, uint Total) GetCompletedHandlerCounts()
    {
        lock (_handlerStatesLock)
        {
            var finished = (uint)_lockedHandlerStates.Count(k => k.Value == SearchRequestHandlerState.Finished);
            var failed = (uint)_lockedHandlerStates.Count(k => k.Value == SearchRequestHandlerState.Failed);
            var pending = (uint)_lockedHandlerStates.Count(k => k.Value == SearchRequestHandlerState.Pending);

            return (finished, failed, pending, finished + failed);
        }
    }
}
