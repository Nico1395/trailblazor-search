namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Service provides the configured <see cref="ISearchEngineOptions"/>.
/// </summary>
public interface ISearchEngineOptionsProvider
{
    /// <summary>
    /// Gets the configured <see cref="ISearchEngineOptions"/>.
    /// </summary>
    /// <returns>The configured <see cref="ISearchEngineOptions"/>.</returns>
    public ISearchEngineOptions GetOptions();
}
