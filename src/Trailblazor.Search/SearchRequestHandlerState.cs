namespace Trailblazor.Search;

/// <summary>
/// State of a search request handler.
/// </summary>
public enum SearchRequestHandlerState
{
    /// <summary>
    /// Handler has yet to report back as either <see cref="Finished"/> or <see cref="Failed"/>.
    /// </summary>
    Pending,

    /// <summary>
    /// Handler has reported back as finished.
    /// </summary>
    Finished,

    /// <summary>
    /// Handler has reported back as failed.
    /// </summary>
    Failed,
}
