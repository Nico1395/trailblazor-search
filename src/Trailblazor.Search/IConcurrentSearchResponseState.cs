namespace Trailblazor.Search;

public interface IConcurrentSearchResponseState
{
    public IReadOnlyDictionary<Guid, SearchRequestHandlerState> HandlerStates { get; }
    public uint GetFinishedHandlerCount();
    public uint GetFailedHandlerCount();
    public uint GetCompletedHandlerCount();
    public double GetCompletedHandlersPercentage();
    public bool IsCompleted();
    internal void SetHandlerState(Guid handlerId, SearchRequestHandlerState handlerState);
}
