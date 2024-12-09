namespace Trailblazor.Search;

/// <summary>
/// Base class for an <see cref="ISearchResult"/>. Implements the <see cref="IEquatable{T}"/> interface using the <see cref="Key"/>.
/// </summary>
public abstract class SearchResult : ISearchResult
{
    /// <inheritdoc/>
    public abstract object Key { get; }

    /// <inheritdoc/>
    public virtual bool Equals(ISearchResult? other)
    {
        if (other == null)
            return false;

        return Key == other.Key;
    }
}
