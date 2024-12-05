namespace Trailblazor.Search;

public sealed record SearchRequestHandlerProgress
{
    public required Guid HandlerId { get; init; }
    public SearchRequestHandlerState HandlerState { get; set; } = SearchRequestHandlerState.Pending;
}
