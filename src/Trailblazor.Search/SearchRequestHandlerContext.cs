using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search;

public record SearchRequestHandlerContext<TRequest>
    where TRequest : class, ISearchRequest
{
    public required TRequest Request { get; init; }
    public required ISearchRequestHandlerConfiguration HandlerConfiguration { get; init; }
}
