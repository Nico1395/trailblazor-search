namespace Trailblazor.Search.DependencyInjection;

internal sealed class SearchEngineOptions : ISearchEngineOptions
{
    private readonly List<ISearchOperationConfiguration> _postRegistrationOperationConfigurations = [];
    internal List<ISearchOperationConfiguration> InternalOperationConfigurations { get; init; } = [];

    public IReadOnlyList<ISearchOperationConfiguration> OperationConfigurations => InternalOperationConfigurations.Concat(_postRegistrationOperationConfigurations).ToList();

    public ISearchOperationConfiguration? GetOperationConfigurationByKey(string operationKey)
    {
        return OperationConfigurations.SingleOrDefault(x => x.Key == operationKey);
    }

    public void AddPipelineConfigurationAfterRegistration(ISearchOperationConfiguration operationConfiguration)
    {
        _postRegistrationOperationConfigurations.Add(operationConfiguration);
    }
}
