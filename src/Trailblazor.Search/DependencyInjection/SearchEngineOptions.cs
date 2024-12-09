namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchEngineOptions : ISearchEngineOptions
{
    private readonly List<ISearchRequestOperationConfiguration> _postRegistrationOperationConfigurations = [];
    internal List<ISearchRequestOperationConfiguration> InternalOperationConfigurations { get; init; } = [];

    public IReadOnlyList<ISearchRequestOperationConfiguration> OperationConfigurations => InternalOperationConfigurations.Concat(_postRegistrationOperationConfigurations).ToList();

    public void AddPipelineConfigurationAfterRegistration(ISearchRequestOperationConfiguration operationConfiguration)
    {
        _postRegistrationOperationConfigurations.Add(operationConfiguration);
    }
}
