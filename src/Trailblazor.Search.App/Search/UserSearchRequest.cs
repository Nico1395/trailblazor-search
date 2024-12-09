namespace Trailblazor.Search.App.Search;

internal sealed record UserSearchRequest : ISearchRequest
{
    public StringSearchCriteria SearchTerm { get; init; } = new();
    public StringSearchCriteria FirstName { get; init; } = new();
    public StringSearchCriteria LastName { get; init; } = new();
    public SearchCriteria<DateTime> Created { get; init; } = new();
    public SearchCriteria<DateTime> LastChanged { get; init; } = new();

    internal static UserSearchRequest Create(UniversalSearchRequest request)
    {
        return new()
        {
            SearchTerm = request.SearchTerm,
            Created = request.Created,
            LastChanged = request.LastChanged,
        };
    }
}
