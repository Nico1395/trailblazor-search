using Microsoft.Extensions.DependencyInjection;
using Trailblazor.Search.Logging;
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

    public static IServiceCollection AddSearchRequestPipelineOperation<TRequest, TRequestPipeline>(this IServiceCollection services, string operationKey, Action<ISearchRequestOperationConfigurationBuilder<TRequest>> pipelineBuilder)
        where TRequest : class, ISearchRequest
        where TRequestPipeline : class, ISearchRequestPipeline<TRequest>
    {
        var builder = new SearchRequestOperationConfigurationBuilder<TRequest>(operationKey, typeof(TRequestPipeline));
        pipelineBuilder.Invoke(builder);
        var pipelineConfiguration = builder.Build();

        RegisterPipelineConfiguration(services, pipelineConfiguration);

        var searchEngineOptions = services.BuildServiceProvider().GetRequiredService<ISearchEngineOptionsProvider>().GetOptions();
        searchEngineOptions.AddPipelineConfigurationAfterRegistration(pipelineConfiguration);

        return services;
    }

    private static void RegisterPipelineConfiguration(IServiceCollection services, ISearchRequestOperationConfiguration pipelineConfiguration)
    {
        // Add the pipeline as a keyed, transient service.
        services.AddKeyedTransient(pipelineConfiguration.PipelineInterfaceType, pipelineConfiguration.Key, pipelineConfiguration.PipelineImplementationType);

        // All its handlers are also to be registered as keyed transient services.
        foreach (var requestHandlerType in pipelineConfiguration.ThreadConfigurations.SelectMany(t => t.RequestHandlerTypes))
            services.AddKeyedTransient(pipelineConfiguration.HandlerInterfaceType, pipelineConfiguration.Key, requestHandlerType);
    }
}
