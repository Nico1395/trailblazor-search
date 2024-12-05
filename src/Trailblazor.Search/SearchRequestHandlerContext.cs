namespace Trailblazor.Search;

public record SearchRequestHandlerContext
{
    public required Guid HandlerId { get; init; }
    public required ISearchRequest Request { get; init; }
}
