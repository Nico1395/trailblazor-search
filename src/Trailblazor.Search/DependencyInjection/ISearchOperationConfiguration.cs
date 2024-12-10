namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Configuration of a search operation.
/// </summary>
public interface ISearchOperationConfiguration
{
    /// <summary>
    /// Key of the operation.
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    /// Interface type of the orchestrating pipeline.
    /// </summary>
    public Type PipelineInterfaceType { get; init; }

    /// <summary>
    /// Implementation type of the orchestrating pipeline.
    /// </summary>
    public Type PipelineImplementationType { get; init; }

    /// <summary>
    /// Interface type of request handlers.
    /// </summary>
    public Type HandlerInterfaceType { get; init; }

    /// <summary>
    /// Thread configurations added to the operation.
    /// </summary>
    public IReadOnlyList<ISearchOperationThreadConfiguration> ThreadConfigurations { get; }
}
