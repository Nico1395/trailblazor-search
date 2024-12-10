using Trailblazor.Search.Criteria;

namespace Trailblazor.Search.App.Search;

internal sealed record SystemLogSearchRequest : ISearchRequest
{
    public StringSearchCriteria SearchTerm { get; init; } = new();
    public StringSearchCriteria Message { get; init; } = new();
    public SearchCriteria<DateTime> Created { get; init; } = new();
    public SearchCriteria<DateTime> LastChanged { get; init; } = new();

    internal static SystemLogSearchRequest Create(UniversalSearchRequest request)
    {
        return new()
        {
            SearchTerm = request.SearchTerm,
            Created = request.Created,
            LastChanged = request.LastChanged,
        };
    }
}
