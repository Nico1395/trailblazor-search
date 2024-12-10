namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationThreadConfigurationBuilder<TRequest> : ISearchOperationThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchOperationThreadConfiguration _threadConfiguration = new();

    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>(int priority = 0)
        where TRequestHandler : class, ISearchRequestHandler<TRequest>
    {
        return WithHandler(typeof(TRequestHandler), priority);
    }

    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType, int priority = 0)
    {
        var handlerConfiguration = new SearchRequestHandlerConfiguration()
        {
            HandlerType = requestHandlerType,
            Priority = priority,
        };
        _threadConfiguration.InternalHandlerConfigurations.Add(handlerConfiguration);

        return this;
    }

    public ISearchOperationThreadConfiguration Build()
    {
        return _threadConfiguration;
    }
}
