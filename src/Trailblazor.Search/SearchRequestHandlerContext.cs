namespace Trailblazor.Search;

public record SearchRequestHandlerContext<TRequest>
    where TRequest : class, ISearchRequest
{
    public required Guid HandlerId { get; init; }
    public required TRequest Request { get; init; }
}
