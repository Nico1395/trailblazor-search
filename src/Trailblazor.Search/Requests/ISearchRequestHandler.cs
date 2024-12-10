using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search.Requests;

/// <summary>
/// Handler that can be invoked in a <see cref="ISearchOperationConfiguration"/>
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public interface ISearchRequestHandler<TRequest>
    where TRequest : class, ISearchRequest
{
    public Task HandleAsync(SearchRequestHandlerContext<TRequest> context, IConcurrentSearchOperationCallback callback, CancellationToken cancellationToken);
}
