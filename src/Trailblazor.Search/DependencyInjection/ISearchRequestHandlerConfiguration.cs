namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Configuration of a request handler.
/// </summary>
public interface ISearchRequestHandlerConfiguration
{
    /// <summary>
    /// Internal ID of the handler.
    /// </summary>
    public Guid HandlerId { get; }

    /// <summary>
    /// Implementation type of the request handler.
    /// </summary>
    public Type HandlerType { get; }

    /// <summary>
    /// Priority or index/order of the handler withing other handlers in its thread.
    /// </summary>
    public int Priority { get; }
}
