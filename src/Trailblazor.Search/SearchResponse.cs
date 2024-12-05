namespace Trailblazor.Search;

public sealed class SearchResponse : ISearchResponse
{
    internal List<ISearchResult> InternalResults { get; set; } = [];
    public IReadOnlyList<ISearchResult> Results => InternalResults;
}
