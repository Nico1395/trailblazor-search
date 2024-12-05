namespace Trailblazor.Search;

public interface ISearchResponse
{
    public IReadOnlyList<ISearchResult> Results { get; }
}
