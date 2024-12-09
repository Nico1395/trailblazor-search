namespace Trailblazor.Search.DependencyInjection;

public interface ISearchEngineOptionsProvider
{
    public ISearchEngineOptions GetOptions();
}
