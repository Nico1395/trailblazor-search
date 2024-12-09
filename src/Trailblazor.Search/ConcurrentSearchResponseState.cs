namespace Trailblazor.Search;

public sealed class ConcurrentSearchResponseState(List<Guid> handlerIds) : IConcurrentSearchResponseState
{
    private readonly Lock _handlerStatesLock = new();
    private readonly Dictionary<Guid, SearchRequestHandlerState> _lockedHandlerStates = handlerIds
        .Select(h => new KeyValuePair<Guid, SearchRequestHandlerState>(h, SearchRequestHandlerState.Pending))
        .ToDictionary();

    public IReadOnlyDictionary<Guid, SearchRequestHandlerState> HandlerStates
    {
        get
        {
            lock (_handlerStatesLock)
                return _lockedHandlerStates;
        }
    }

    public uint GetFinishedHandlerCount()
    {
        return GetCompletedHandlerCounts().Finished;
    }

    public uint GetFailedHandlerCount()
    {
        return GetCompletedHandlerCounts().Failed;
    }

    public uint GetCompletedHandlerCount()
    {
        return GetCompletedHandlerCounts().Total;
    }

    public double GetCompletedHandlersPercentage()
    {
        var completedHandlerCount = GetCompletedHandlerCount();
        lock (_handlerStatesLock)
            return completedHandlerCount / _lockedHandlerStates.Count;
    }

    public bool IsCompleted()
    {
        return GetCompletedHandlersPercentage() == 1;
    }

    public void SetHandlerState(Guid handlerId, SearchRequestHandlerState handlerState)
    {
        lock (_handlerStatesLock)
            _lockedHandlerStates[handlerId] = handlerState;
    }

    private (uint Finished, uint Failed, uint Total) GetCompletedHandlerCounts()
    {
        lock (_handlerStatesLock)
        {
            uint finished = (uint)_lockedHandlerStates.Count(k => k.Value == SearchRequestHandlerState.Finished);
            uint failed = (uint)_lockedHandlerStates.Count(k => k.Value == SearchRequestHandlerState.Failed);

            return (finished, failed, finished + failed);
        }
    }
}
