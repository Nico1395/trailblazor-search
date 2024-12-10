namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Configuration of a thread in a search operation.
/// </summary>
public interface ISearchOperationThreadConfiguration
{
    /// <summary>
    /// Configured request handlers.
    /// </summary>
    public IReadOnlyList<ISearchRequestHandlerConfiguration> HandlerConfigurations { get; }
}
