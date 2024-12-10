namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Options for the search engine.
/// </summary>
public interface ISearchEngineOptions
{
    /// <summary>
    /// Search operation configurations that can be handled.
    /// </summary>
    public IReadOnlyList<ISearchOperationConfiguration> OperationConfigurations { get; }

    /// <summary>
    /// Gets the <see cref="ISearchOperationConfiguration"/> for the given <paramref name="operationKey"/>.
    /// </summary>
    /// <param name="operationKey">Key of the target configuration.</param>
    /// <returns>The <see cref="ISearchOperationConfiguration"/> for the given <paramref name="operationKey"/>.</returns>
    public ISearchOperationConfiguration? GetOperationConfigurationByKey(string operationKey);

    /// <summary>
    /// Adds the given <paramref name="operationConfiguration"/> to the options post build.
    /// </summary>
    /// <param name="operationConfiguration">Configuration to be added.</param>
    internal void AddPipelineConfigurationAfterRegistration(ISearchOperationConfiguration operationConfiguration);
}
