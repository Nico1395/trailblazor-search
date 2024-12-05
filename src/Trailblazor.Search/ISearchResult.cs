namespace Trailblazor.Search;

public interface ISearchResult : IEquatable<ISearchResult>
{
    public string Key { get; }
}
