using Trailblazor.Search.DependencyInjection;
using Trailblazor.Search.Requests;

namespace Trailblazor.Search;

public record SearchRequestHandlerContext<TRequest>
    where TRequest : class, ISearchRequest
{
    public required TRequest Request { get; init; }
    public required ISearchRequestHandlerConfiguration HandlerConfiguration { get; init; }
}
