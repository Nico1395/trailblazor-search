namespace Trailblazor.Search;

/// <summary>
/// Event arguments for when the <see cref="IConcurrentSearchResponse"/> of a search operation has changed.
/// </summary>
/// <param name="response">Response that has changed.</param>
public sealed class SearchResponseChangedEventArgs(IConcurrentSearchResponse response) : EventArgs
{
    /// <summary>
    /// The response that has changed.
    /// </summary>
    public IConcurrentSearchResponse Response { get; } = response;
}
