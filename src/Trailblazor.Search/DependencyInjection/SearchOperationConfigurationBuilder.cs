namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationConfigurationBuilder<TRequest>(string operationKey, Type pipelineImplementationType) : ISearchOperationConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchOperationConfiguration _operationConfiguration = new()
    {
        Key = operationKey,
        PipelineInterfaceType = typeof(ISearchRequestPipeline<TRequest>),
        PipelineImplementationType = pipelineImplementationType,
        HandlerInterfaceType = typeof(ISearchRequestHandler<TRequest>),
    };

    public ISearchOperationConfigurationBuilder<TRequest> WithThread(Action<ISearchOperationThreadConfigurationBuilder<TRequest>> threadBuilder)
    {
        var builder = new SearchOperationThreadConfigurationBuilder<TRequest>();
        threadBuilder.Invoke(builder);

        var threadConfiguration = builder.Build();
        _operationConfiguration.InternalThreadConfigurations.Add(threadConfiguration);

        return this;
    }

    public ISearchOperationConfiguration Build()
    {
        return _operationConfiguration;
    }
}