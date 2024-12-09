namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchRequestThreadConfigurationBuilder<TRequest> : ISearchRequestThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchRequestThreadConfiguration _threadConfiguration = new();

    public ISearchRequestThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>()
        where TRequestHandler : class, ISearchRequestHandler<TRequest>
    {
        return WithHandler(typeof(TRequestHandler));
    }

    public ISearchRequestThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType)
    {
        if (!_threadConfiguration.InternalRequestHandlerTypes.Contains(requestHandlerType))
            _threadConfiguration.InternalRequestHandlerTypes.Add(requestHandlerType);

        return this;
    }

    public ISearchRequestThreadConfigurationBuilder<TRequest> WithPriority(int priority)
    {
        _threadConfiguration.Priority = priority;
        return this;
    }

    public ISearchRequestThreadConfiguration Build()
    {
        return _threadConfiguration;
    }
}
