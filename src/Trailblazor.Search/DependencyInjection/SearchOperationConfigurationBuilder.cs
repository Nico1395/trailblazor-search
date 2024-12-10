namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchOperationConfigurationBuilder<TRequest>(string operationKey) : ISearchOperationConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    private readonly SearchOperationConfiguration _operationConfiguration = new()
    {
        Key = operationKey,
        PipelineInterfaceType = typeof(ISearchRequestPipeline<TRequest>),
        PipelineImplementationType = typeof(DefaultSearchRequestPipeline<TRequest>),
        HandlerInterfaceType = typeof(ISearchRequestHandler<TRequest>),
    };

    public ISearchOperationConfigurationBuilder<TRequest> WithThread(string threadKey, Action<ISearchOperationThreadConfigurationBuilder<TRequest>> threadBuilder)
    {
        var builder = new SearchOperationThreadConfigurationBuilder<TRequest>(threadKey);
        threadBuilder.Invoke(builder);

        var threadConfiguration = builder.Build();
        _operationConfiguration.InternalThreadConfigurations.Add(threadConfiguration);

        return this;
    }

    public ISearchOperationConfigurationBuilder<TRequest> UseCallback<TCallback>()
        where TCallback : IConcurrentSearchOperationCallback
    {
        return UseCallback(typeof(TCallback));
    }

    public ISearchOperationConfigurationBuilder<TRequest> UseCallback(Type callbackImplementationType)
    {
        _operationConfiguration.CallbackImplementationType = callbackImplementationType;
        return this;
    }

    public ISearchOperationConfigurationBuilder<TRequest> HandledByPipeline<TPipeline>()
        where TPipeline : ISearchRequestPipeline<TRequest>
    {
        return HandledByPipeline(typeof(TPipeline));
    }

    public ISearchOperationConfigurationBuilder<TRequest> HandledByPipeline(Type pipelineImplementationType)
    {
        _operationConfiguration.PipelineImplementationType = pipelineImplementationType;
        return this;
    }

    public ISearchOperationConfiguration Build()
    {
        return _operationConfiguration;
    }
}