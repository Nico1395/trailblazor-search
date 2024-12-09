namespace Trailblazor.Search;

public interface IConcurrentSearchResponse
{
    public IReadOnlyList<ISearchResult> Results { get; }
    public IConcurrentSearchResponseState State { get; }
}
