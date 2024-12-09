namespace Trailblazor.Search;

/// <summary>
/// Thread-safe search response of a search operation.
/// </summary>
public interface IConcurrentSearchResponse
{
    /// <summary>
    /// Results of the response.
    /// </summary>
    public IReadOnlyList<ISearchResult> Results { get; }

    /// <summary>
    /// State of the response. Contains information about the progresses of the operation.
    /// </summary>
    public IConcurrentSearchResponseState State { get; }
}
