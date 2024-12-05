namespace Trailblazor.Search;

public abstract class SearchResult : ISearchResult
{
    public abstract string Key { get; }

    public virtual bool Equals(ISearchResult? other)
    {
        if (other == null)
            return false;

        return Key == other.Key;
    }
}
