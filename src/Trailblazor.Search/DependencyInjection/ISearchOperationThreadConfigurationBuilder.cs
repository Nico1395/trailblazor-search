namespace Trailblazor.Search.DependencyInjection;

public interface ISearchOperationThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithPriority(int priority);
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>()
        where TRequestHandler : class, ISearchRequestHandler<TRequest>;
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType);
    internal ISearchOperationThreadConfiguration Build();
}
