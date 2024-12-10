namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Builder for <see cref="ISearchOperationConfiguration"/>s.
/// </summary>
/// <typeparam name="TRequest">Type of request being handled in the operation.</typeparam>
public interface ISearchOperationConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    /// <summary>
    /// Adds a <see cref="ISearchOperationThreadConfiguration"/> to the operation.
    /// </summary>
    /// <param name="threadKey">Key of the thread. Unique in the operation.</param>
    /// <param name="threadBuilder">Builder action for configuring the thread.</param>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationConfigurationBuilder<TRequest> WithThread(string threadKey, Action<ISearchOperationThreadConfigurationBuilder<TRequest>> threadBuilder);

    /// <summary>
    /// Sets the specified <typeparamref name="TCallback"/> to be used as the <see cref="IConcurrentSearchOperationCallback"/> implementation.
    /// </summary>
    /// <typeparam name="TCallback">Type of callback implemenation.</typeparam>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationConfigurationBuilder<TRequest> UseCallback<TCallback>()
        where TCallback : IConcurrentSearchOperationCallback;

    /// <summary>
    /// Sets the specified <paramref name="callbackImplementationType"/> to be used as the <see cref="IConcurrentSearchOperationCallback"/> implementation.
    /// </summary>
    /// <param name="callbackImplementationType">Type of callback implemenation.</param>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationConfigurationBuilder<TRequest> UseCallback(Type callbackImplementationType);

    /// <summary>
    /// Sets the specified <typeparamref name="TPipeline"/> to orchestrate the operation.
    /// </summary>
    /// <typeparam name="TPipeline">Type of pipeline orchestration the operation.</typeparam>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationConfigurationBuilder<TRequest> HandledByPipeline<TPipeline>()
        where TPipeline : ISearchRequestPipeline<TRequest>;

    /// <summary>
    /// Sets the specified <paramref name="pipelineImplementationType"/> to orchestrate the operation.
    /// </summary>
    /// <param name="pipelineImplementationType">Type of pipeline orchestration the operation.</param>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationConfigurationBuilder<TRequest> HandledByPipeline(Type pipelineImplementationType);

    /// <summary>
    /// Completes the configuration process for a <see cref="ISearchOperationConfiguration"/>.
    /// </summary>
    /// <returns>The configured <see cref="ISearchOperationConfiguration"/>.</returns>
    internal ISearchOperationConfiguration Build();
}
