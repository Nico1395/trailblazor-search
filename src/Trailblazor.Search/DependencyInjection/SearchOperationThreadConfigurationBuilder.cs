namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationThreadConfigurationBuilder<TRequest> : ISearchOperationThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchOperationThreadConfiguration _threadConfiguration = new();

    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>()
        where TRequestHandler : class, ISearchRequestHandler<TRequest>
    {
        return WithHandler(typeof(TRequestHandler));
    }

    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType)
    {
        if (!_threadConfiguration.InternalRequestHandlerTypes.Contains(requestHandlerType))
            _threadConfiguration.InternalRequestHandlerTypes.Add(requestHandlerType);

        return this;
    }

    public ISearchOperationThreadConfigurationBuilder<TRequest> WithPriority(int priority)
    {
        _threadConfiguration.Priority = priority;
        return this;
    }

    public ISearchOperationThreadConfiguration Build()
    {
        return _threadConfiguration;
    }
}
