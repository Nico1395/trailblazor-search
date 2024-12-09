namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchRequestOperationConfigurationBuilder<TRequest>(string operationKey, Type pipelineImplementationType) : ISearchRequestOperationConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchRequestOperationConfiguration _operationConfiguration = new()
    {
        Key = operationKey,
        PipelineInterfaceType = typeof(ISearchRequestPipeline<TRequest>),
        PipelineImplementationType = pipelineImplementationType,
        HandlerInterfaceType = typeof(ISearchRequestHandler<TRequest>),
    };

    public ISearchRequestOperationConfigurationBuilder<TRequest> WithThread(Action<ISearchRequestThreadConfigurationBuilder<TRequest>> threadBuilder)
    {
        var builder = new SearchRequestThreadConfigurationBuilder<TRequest>();
        threadBuilder.Invoke(builder);

        var threadConfiguration = builder.Build();
        _operationConfiguration.InternalThreadConfigurations.Add(threadConfiguration);

        return this;
    }

    public ISearchRequestOperationConfiguration Build()
    {
        return _operationConfiguration;
    }
}