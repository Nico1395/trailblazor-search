namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchEngineOptionsBuilder : ISearchEngineOptionsBuilder
{
    private readonly SearchEngineOptions _options = new();

    public ISearchEngineOptionsBuilder AddOperation<TRequest>(string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
    {
        return AddOperation(typeof(DefaultSearchRequestPipeline<TRequest>), operationKey, operationBuilder);
    }

    public ISearchEngineOptionsBuilder AddOperation<TRequest, TPipeline>(string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
        where TPipeline : class, ISearchRequestPipeline<TRequest>
    {
        return AddOperation(typeof(TPipeline), operationKey, operationBuilder);
    }

    public ISearchEngineOptionsBuilder AddOperation<TRequest>(Type pipelineImplementationType, string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
    {
        var builder = new SearchRequestOperationConfigurationBuilder<TRequest>(operationKey, pipelineImplementationType);
        operationBuilder.Invoke(builder);

        var operationConfiguration = builder.Build();
        _options.InternalOperationConfigurations.Add(operationConfiguration);

        return this;
    }

    public ISearchEngineOptions Build()
    {
        return _options;
    }
}