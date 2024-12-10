using Trailblazor.Search.Requests;

namespace Trailblazor.Search.DependencyInjection;

public interface ISearchOperationConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    public ISearchOperationConfigurationBuilder<TRequest> WithThread(Action<ISearchOperationThreadConfigurationBuilder<TRequest>> threadBuilder);
    internal ISearchOperationConfiguration Build();
}
