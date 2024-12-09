namespace Trailblazor.Search;

public interface ISearchResult : IEquatable<ISearchResult>
{
    public object Key { get; }
}
