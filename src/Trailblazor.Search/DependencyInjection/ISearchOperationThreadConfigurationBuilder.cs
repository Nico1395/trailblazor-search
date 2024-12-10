namespace Trailblazor.Search.DependencyInjection;

public interface ISearchOperationThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>(int priority = 0)
        where TRequestHandler : class, ISearchRequestHandler<TRequest>;
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType, int priority = 0);
    internal ISearchOperationThreadConfiguration Build();
}
