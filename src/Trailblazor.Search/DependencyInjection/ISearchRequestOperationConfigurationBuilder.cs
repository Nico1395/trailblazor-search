namespace Trailblazor.Search.DependencyInjection;

public interface ISearchRequestOperationConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    public ISearchRequestOperationConfigurationBuilder<TRequest> WithThread(Action<ISearchRequestThreadConfigurationBuilder<TRequest>> threadBuilder);
    internal ISearchRequestOperationConfiguration Build();
}
