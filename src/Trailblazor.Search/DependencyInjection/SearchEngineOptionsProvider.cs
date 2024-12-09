namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchEngineOptionsProvider(ISearchEngineOptions _searchEngineOptions) : ISearchEngineOptionsProvider
{
    public ISearchEngineOptions GetOptions()
    {
        return _searchEngineOptions;
    }
}
