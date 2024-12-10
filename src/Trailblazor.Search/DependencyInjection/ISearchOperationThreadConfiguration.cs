namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Configuration of a thread in a search operation.
/// </summary>
public interface ISearchOperationThreadConfiguration
{
    /// <summary>
    /// Key of the thread. Unique to the operation.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Configured request handlers.
    /// </summary>
    public IReadOnlyList<ISearchRequestHandlerConfiguration> HandlerConfigurations { get; }
}
