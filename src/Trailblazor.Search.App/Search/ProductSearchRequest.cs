namespace Trailblazor.Search.App.Search;

internal sealed record ProductSearchRequest : ISearchRequest
{
    public StringSearchCriteria SearchTerm { get; init; } = new();
    public SearchCriteria<int> InStock { get; init; } = new();
    public SearchCriteria<int> Sold { get; init; } = new();
    public SearchCriteria<DateTime> Created { get; init; } = new();
    public SearchCriteria<DateTime> LastChanged { get; init; } = new();

    internal static ProductSearchRequest Create(UniversalSearchRequest request)
    {
        return new()
        {
            SearchTerm = request.SearchTerm,
            Created = request.Created,
            LastChanged = request.LastChanged,
        };
    }
}
