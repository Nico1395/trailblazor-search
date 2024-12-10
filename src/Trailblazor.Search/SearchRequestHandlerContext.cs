using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search;

/// <summary>
/// Context of a search request handler.
/// </summary>
/// <typeparam name="TRequest">Type of request to be handled.</typeparam>
public sealed record SearchRequestHandlerContext<TRequest>
    where TRequest : class, ISearchRequest
{
    /// <summary>
    /// Request to be handled by the request handler.
    /// </summary>
    public required TRequest Request { get; init; }

    /// <summary>
    /// Configuration of the request handler.
    /// </summary>
    public required ISearchRequestHandlerConfiguration HandlerConfiguration { get; init; }
}
