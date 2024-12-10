namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Builder for <see cref="ISearchEngineOptions"/>.
/// </summary>
public interface ISearchEngineOptionsBuilder
{
    /// <summary>
    /// Adds a search operation to the options.
    /// </summary>
    /// <typeparam name="TRequest">Type of search request being handled by the operation.</typeparam>
    /// <param name="operationKey">Unique key of the operation.</param>
    /// <param name="operationBuilder">Builder action for an <see cref="ISearchOperationConfiguration"/>.</param>
    /// <returns>The <see cref="ISearchEngineOptionsBuilder"/> for further configurations.</returns>
    public ISearchEngineOptionsBuilder AddOperation<TRequest>(string operationKey, Action<ISearchOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest;

    /// <summary>
    /// Adds a search operation to the options.
    /// </summary>
    /// <typeparam name="TRequest">Type of search request being handled by the operation.</typeparam>
    /// <typeparam name="TPipeline">Type of pipeline orchestrating the operation.</typeparam>
    /// <param name="operationKey">Unique key of the operation.</param>
    /// <param name="operationBuilder">Builder action for an <see cref="ISearchOperationConfiguration"/>.</param>
    /// <returns>The <see cref="ISearchEngineOptionsBuilder"/> for further configurations.</returns>
    public ISearchEngineOptionsBuilder AddOperation<TRequest, TPipeline>(string operationKey, Action<ISearchOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
        where TPipeline : class, ISearchRequestPipeline<TRequest>;

    /// <summary>
    /// Adds a search operation to the options.
    /// </summary>
    /// <typeparam name="TRequest">Type of search request being handled by the operation.</typeparam>
    /// <param name="pipelineImplementationType">Type of the pipeline orchestrating the operation.</param>
    /// <param name="operationKey">Unique key of the operation.</param>
    /// <param name="operationBuilder">Builder action for an <see cref="ISearchOperationConfiguration"/>.</param>
    /// <returns>The <see cref="ISearchEngineOptionsBuilder"/> for further configurations.</returns>
    public ISearchEngineOptionsBuilder AddOperation<TRequest>(Type pipelineImplementationType, string operationKey, Action<ISearchOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest;

    /// <summary>
    /// Completes the configuration process and builds a <see cref="ISearchEngineOptions"/>.
    /// </summary>
    /// <returns>The configured <see cref="ISearchEngineOptions"/>.</returns>
    internal ISearchEngineOptions Build();
}
