namespace Trailblazor.Search;

public sealed class SearchResponseChangedEventArgs(IConcurrentSearchResponse response) : EventArgs
{
    public IConcurrentSearchResponse Response { get; } = response;
}
