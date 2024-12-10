using Trailblazor.Search.Requests;

namespace Trailblazor.Search;

/// <summary>
/// State of an <see cref="IConcurrentSearchResponse"/>. Contains information about the progresses of an operation.
/// </summary>
public interface IConcurrentSearchResponseState
{
    /// <summary>
    /// States of the <see cref="ISearchRequestHandler{TRequest}"/>.
    /// </summary>
    public IReadOnlyDictionary<Guid, SearchRequestHandlerState> HandlerStates { get; }

    /// <summary>
    /// Gets the count of the request handlers that have finished.
    /// </summary>
    /// <returns>Count of finished request handlers.</returns>
    public uint GetFinishedHandlerCount();

    /// <summary>
    /// Gets the count of the request handlers that have failed and ran into an error.
    /// </summary>
    /// <returns>Count of failed request handlers.</returns>
    public uint GetFailedHandlerCount();

    /// <summary>
    /// Gets the count of the request handlers that have either failed and ran into an error or finished.
    /// </summary>
    /// <returns>Count of either failed or finished request handlers.</returns>
    public uint GetCompletedHandlerCount();

    /// <summary>
    /// Gets the count of the request handlers that have not reported back yet.
    /// </summary>
    /// <returns>Count of request handlers that have not reported back yet.</returns>
    public uint GetPendingHandlerCount();

    /// <summary>
    /// Gets the count of the request handlers that have either failed and ran into an error or finished as a percentage.
    /// </summary>
    /// <remarks>
    /// Use this method for things such as status bars or alike.
    /// </remarks>
    /// <returns>Count of the request handlers that have either failed and ran into an error or finished as a percentage.</returns>
    public double GetCompletedHandlersPercentage();

    /// <summary>
    /// Determines whether the search operation has completed or not.
    /// </summary>
    /// <remarks>
    /// If this is <see langword="true"/> all request handlers have completed their work, so either failed or finished.
    /// </remarks>
    /// <returns><see langword="true"/> if all request handlers have completed their work, so either failed or finished</returns>
    public bool IsCompleted();

    /// <summary>
    /// Internally sets a handlers <see cref="SearchRequestHandlerState"/>.
    /// </summary>
    /// <param name="handlerId">ID of the <see cref="ISearchRequestHandler{TRequest}"/>.</param>
    /// <param name="handlerState">State to be set to the handler.</param>
    internal void SetHandlerState(Guid handlerId, SearchRequestHandlerState handlerState);
}
