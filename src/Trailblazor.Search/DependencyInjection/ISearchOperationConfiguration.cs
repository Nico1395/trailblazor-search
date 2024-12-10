namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Configuration of a search operation.
/// </summary>
public interface ISearchOperationConfiguration
{
    /// <summary>
    /// Key of the operation.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Interface type of the orchestrating pipeline.
    /// </summary>
    public Type PipelineInterfaceType { get; }

    /// <summary>
    /// Implementation type of the orchestrating pipeline.
    /// </summary>
    public Type PipelineImplementationType { get; }

    /// <summary>
    /// Interface type of request handlers.
    /// </summary>
    public Type HandlerInterfaceType { get; }

    /// <summary>
    /// Implementation type of the operation callback.
    /// </summary>
    public Type? CallbackImplementationType { get; }

    /// <summary>
    /// Thread configurations added to the operation.
    /// </summary>
    public IReadOnlyList<ISearchOperationThreadConfiguration> ThreadConfigurations { get; }
}
