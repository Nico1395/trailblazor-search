namespace Trailblazor.Search.DependencyInjection;

public interface ISearchEngineOptions
{
    public IReadOnlyList<ISearchRequestOperationConfiguration> OperationConfigurations { get; }
    internal void AddPipelineConfigurationAfterRegistration(ISearchRequestOperationConfiguration operationConfiguration);
}
