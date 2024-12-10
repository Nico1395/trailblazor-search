using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search;

/// <summary>
/// Pipeline running and orchestrating an <see cref="ISearchOperationConfiguration"/>.
/// </summary>
/// <typeparam name="TRequest">Type of request to be handled in the context of an operation.</typeparam>
public interface ISearchRequestPipeline<TRequest>
    where TRequest : class, ISearchRequest
{
    /// <summary>
    /// Runs the given <paramref name="searchOperationConfiguration"/> with the given <paramref name="request"/>.
    /// </summary>
    /// <param name="searchOperationConfiguration">Configuration of the target search operation.</param>
    /// <param name="request">Search request used in the operation.</param>
    /// <param name="cancellationToken">Cancellation token for aborting the operation.</param>
    /// <returns>Concurrent search operation callback for subscribing to the changing search result.</returns>
    public Task<IConcurrentSearchOperationCallback> RunPipelineForOperationAsync(ISearchOperationConfiguration searchOperationConfiguration, TRequest request, CancellationToken cancellationToken);
}
