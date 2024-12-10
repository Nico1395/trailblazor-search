namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Builder for a <see cref="ISearchOperationThreadConfiguration"/>.
/// </summary>
/// <typeparam name="TRequest">Type of request being handled in the operation.</typeparam>
public interface ISearchOperationThreadConfigurationBuilder<TRequest>
    where TRequest : class, ISearchRequest
{
    /// <summary>
    /// Adds a request handler to the thread.
    /// </summary>
    /// <typeparam name="TRequestHandler">Type of request handler handing the <typeparamref name="TRequest"/>.</typeparam>
    /// <param name="priority">Priority or index/order within other handlers in its thread.</param>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler<TRequestHandler>(int priority = 0)
        where TRequestHandler : class, ISearchRequestHandler<TRequest>;

    /// <summary>
    /// Adds a request handler to the thread.
    /// </summary>
    /// <param name="requestHandlerType">Type of request handler handing the <typeparamref name="TRequest"/>.</param>
    /// <param name="priority">Priority or index/order within other handlers in its thread.</param>
    /// <returns>The <see cref="ISearchOperationThreadConfigurationBuilder{TRequest}"/> for further configurations.</returns>
    public ISearchOperationThreadConfigurationBuilder<TRequest> WithHandler(Type requestHandlerType, int priority = 0);

    /// <summary>
    /// Completes the configuration process of the configured <see cref="ISearchOperationThreadConfiguration"/>.
    /// </summary>
    /// <returns>The configured <see cref="ISearchOperationThreadConfiguration"/>.</returns>
    internal ISearchOperationThreadConfiguration Build();
}
