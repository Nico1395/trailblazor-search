namespace Trailblazor.Search;

public sealed class SearchRequestProgress(List<Guid> handlerIds)
{
    private readonly Dictionary<Guid, SearchRequestHandlerState> _handlerStates = handlerIds.ToDictionary(k => k, v => SearchRequestHandlerState.Pending);

    public IReadOnlyDictionary<Guid, SearchRequestHandlerState> HandlerStates => _handlerStates;
    public uint FinishedHandlerCount => (uint)_handlerStates.Where(k => k.Value == SearchRequestHandlerState.Finished).Count();
    public uint FailedHandlerCount => (uint)_handlerStates.Where(k => k.Value == SearchRequestHandlerState.Finished).Count();
    public uint CompletedHandlerCount => FinishedHandlerCount + FailedHandlerCount;
    public double HandlersCompletedPercentage => CompletedHandlerCount / HandlerStates.Count;

    internal void SetHandlerState(Guid handlerId, SearchRequestHandlerState handlerState)
    {
        _handlerStates[handlerId] = handlerState;
    }
}
