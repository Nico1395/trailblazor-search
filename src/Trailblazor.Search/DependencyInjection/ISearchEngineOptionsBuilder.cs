namespace Trailblazor.Search.DependencyInjection;

public interface ISearchEngineOptionsBuilder
{
    public ISearchEngineOptionsBuilder AddOperation<TRequest>(string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest;

    public ISearchEngineOptionsBuilder AddOperation<TRequest, TPipeline>(string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
        where TPipeline : class, ISearchRequestPipeline<TRequest>;

    public ISearchEngineOptionsBuilder AddOperation<TRequest>(Type pipelineImplementationType, string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest;

    internal ISearchEngineOptions Build();
}
