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
    /// <param name="threadBuilder">Builder action for configuring the thread.</param>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationConfigurationBuilder<TRequest> WithThread(Action<ISearchOperationThreadConfigurationBuilder<TRequest>> threadBuilder);

    /// <summary>
    /// Completes the configuration process for a <see cref="ISearchOperationConfiguration"/>.
    /// </summary>
    /// <returns>The configured <see cref="ISearchOperationConfiguration"/>.</returns>
    internal ISearchOperationConfiguration Build();
}
