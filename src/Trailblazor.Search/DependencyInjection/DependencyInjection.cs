using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.Logging;
using Trailblazor.Search.Requests;
using Trailblazor.Search.Workers;

namespace Trailblazor.Search.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddTrailblazorSearchClient(this IServiceCollection services, Action<ISearchEngineOptionsBuilder>? options = null)
    {
        var builder = new SearchEngineOptionsBuilder();
        options?.Invoke(builder);
        var searchEngineOptions = builder.Build();

        services.AddSingleton<ISearchEngineOptionsProvider>(sp => new SearchEngineOptionsProvider(searchEngineOptions));
        services.AddTransient<ICallbackLoggingHandler, CallbackLoggingHandler>();
        services.AddTransient<ISearchWorker, SearchWorker>();
        services.AddTransient<ISearchEngine, SearchEngine>();

        foreach (var pipelineConfiguration in searchEngineOptions.OperationConfigurations)
            RegisterPipelineConfiguration(services, pipelineConfiguration);

        return services;
    }

    public static IServiceCollection AddSearchRequestPipelineOperation<TRequest, TRequestPipeline>(this IServiceCollection services, string operationKey, Action<ISearchOperationConfigurationBuilder<TRequest>> pipelineBuilder)
        where TRequest : class, ISearchRequest
        where TRequestPipeline : class, ISearchRequestPipeline<TRequest>
    {
        var builder = new SearchOperationConfigurationBuilder<TRequest>(operationKey, typeof(TRequestPipeline));
        pipelineBuilder.Invoke(builder);
        var operationConfiguration = builder.Build();

        RegisterPipelineConfiguration(services, operationConfiguration);

        var searchEngineOptions = services.BuildServiceProvider().GetRequiredService<ISearchEngineOptionsProvider>().GetOptions();
        searchEngineOptions.AddPipelineConfigurationAfterRegistration(operationConfiguration);

        return services;
    }

    private static void RegisterPipelineConfiguration(IServiceCollection services, ISearchOperationConfiguration operationConfiguration)
    {
        // Add the pipeline as a keyed, transient service.
        services.AddKeyedTransient(operationConfiguration.PipelineInterfaceType, operationConfiguration.Key, operationConfiguration.PipelineImplementationType);

        // All its handlers are also to be registered as keyed transient services.
        foreach (var handlerConfiguration in operationConfiguration.ThreadConfigurations.SelectMany(t => t.HandlerConfigurations))
            services.AddKeyedTransient(operationConfiguration.HandlerInterfaceType, operationConfiguration.Key, handlerConfiguration.HandlerType);
    }
}
