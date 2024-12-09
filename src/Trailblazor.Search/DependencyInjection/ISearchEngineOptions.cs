namespace Trailblazor.Search.DependencyInjection;

public interface ISearchEngineOptions
{
    public IReadOnlyList<ISearchOperationConfiguration> OperationConfigurations { get; }
    public ISearchOperationConfiguration? GetOperationConfigurationByKey(string operationKey);
    internal void AddPipelineConfigurationAfterRegistration(ISearchOperationConfiguration operationConfiguration);
}
