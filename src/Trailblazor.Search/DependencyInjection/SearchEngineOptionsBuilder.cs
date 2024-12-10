namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchEngineOptionsBuilder : ISearchEngineOptionsBuilder
{
    private readonly SearchEngineOptions _options = new();

    public ISearchEngineOptionsBuilder AddOperation<TRequest>(string operationKey, Action<ISearchOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
    {
        var builder = new SearchOperationConfigurationBuilder<TRequest>(operationKey);
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