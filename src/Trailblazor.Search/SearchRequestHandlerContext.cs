namespace Trailblazor.Search;

public record SearchRequestHandlerContext<TRequest>
    where TRequest : class, ISearchRequest
{
    public required TRequest Request { get; init; }
    public required SearchRequestHandlerMetadata Metadata { get; init; }
}
