namespace Trailblazor.Search.DependencyInjection;

public interface ISearchRequestThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    public ISearchRequestThreadConfigurationBuilder<TRequest> WithPriority(int priority);
    public ISearchRequestThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>()
        where TRequestHandler : class, ISearchRequestHandler<TRequest>;
    public ISearchRequestThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType);
    internal ISearchRequestThreadConfiguration Build();
}
