namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationThreadConfigurationBuilder<TRequest>(string threadKey) : ISearchOperationThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchOperationThreadConfiguration _threadConfiguration = new()
    {
        Key = threadKey,
    };

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
