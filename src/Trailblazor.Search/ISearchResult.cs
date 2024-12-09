namespace Trailblazor.Search;

/// <summary>
/// Search result of a search operation.
/// </summary>
public interface ISearchResult : IEquatable<ISearchResult>
{
    /// <summary>
    /// Unique key of the result. This might be compared to other results to keep results distinct.
    /// </summary>
    public object Key { get; }
}
