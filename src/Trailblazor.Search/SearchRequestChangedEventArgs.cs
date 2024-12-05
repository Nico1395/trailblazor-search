namespace Trailblazor.Search;

public class SearchRequestChangedEventArgs : EventArgs
{
    public required virtual ISearchResponse Response { get; init; }
    public required virtual SearchRequestProgress Progress { get; init; }
}
