using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.Criteria.Workers;
using Trailblazor.Search.Logging;

namespace Trailblazor.Search.DependencyInjection;

/// <summary>
/// Static class contains extensions for setting up dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the Trailblazor search services to the <paramref name="services"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> the services are to be registered to.</param>
    /// <param name="options">Builder action for configuring the search engines <see cref="ISearchEngineOptions"/>.</param>
    /// <returns>The <paramref name="services"/> for further configurations.</returns>
    public static IServiceCollection AddTrailblazorSearch(this IServiceCollection services, Action<ISearchEngineOptionsBuilder>? options = null)
    {
        var builder = new SearchEngineOptionsBuilder();
        options?.Invoke(builder);
        var searchEngineOptions = builder.Build();

        services.AddSingleton<ISearchEngineOptionsProvider>(sp => new SearchEngineOptionsProvider(searchEngineOptions));
        services.AddTransient<ICallbackLoggingHandler, CallbackLoggingHandler>();
        services.AddTransient<IStringSearchCriteriaWorker, StringSearchCriteriaWorker>();
        services.AddTransient<ISearchEngine, SearchEngine>();

        foreach (var pipelineConfiguration in searchEngineOptions.OperationConfigurations)
            RegisterPipelineConfiguration(services, pipelineConfiguration);

        return services;
    }

    /// <summary>
    /// Adds a search request to the <paramref name="services"/>.
    /// </summary>
    /// <typeparam name="TRequest">Type of request to be handled by the operation.</typeparam>
    /// <param name="services"><see cref="IServiceCollection"/> the services are to be registered to.</param>
    /// <param name="operationKey">Key of the operation.</param>
    /// <param name="operationBuilder">Builder action for configuring the operation.</param>
    /// <returns>The <paramref name="services"/> for further configurations.</returns>
    public static IServiceCollection AddSearchRequestOperation<TRequest>(this IServiceCollection services, string operationKey, Action<ISearchOperationConfigurationBuilder<TRequest>> operationBuilder)
        where TRequest : class, ISearchRequest
    {
        var builder = new SearchOperationConfigurationBuilder<TRequest>(operationKey);
        operationBuilder.Invoke(builder);
        var operationConfiguration = builder.Build();

        RegisterPipelineConfiguration(services, operationConfiguration);

        var searchEngineOptions = services.BuildServiceProvider().GetRequiredService<ISearchEngineOptionsProvider>().GetOptions();
        searchEngineOptions.AddPipelineConfigurationAfterRegistration(operationConfiguration);

        return services;
    }

    private static void RegisterPipelineConfiguration(IServiceCollection services, ISearchOperationConfiguration operationConfiguration)
    {
        services.AddKeyedTransient(operationConfiguration.PipelineInterfaceType, operationConfiguration.Key, operationConfiguration.PipelineImplementationType);
        services.AddKeyedScoped(typeof(IConcurrentSearchOperationCallback), operationConfiguration.Key, operationConfiguration.CallbackImplementationType ?? typeof(ConcurrentSearchOperationCallback));

        foreach (var handlerConfiguration in operationConfiguration.ThreadConfigurations.SelectMany(t => t.HandlerConfigurations))
            services.AddKeyedTransient(operationConfiguration.HandlerInterfaceType, operationConfiguration.Key, handlerConfiguration.HandlerType);
    }
}
