namespace Trailblazor.Search;

public sealed record SearchRequestHandlerMetadata
{
    public required Guid HandlerId { get; init; }
    public required Type HandlerType { get; init; }
}
