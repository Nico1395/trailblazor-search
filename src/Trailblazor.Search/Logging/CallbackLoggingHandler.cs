using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trailblazor.Search.DependencyInjection;

namespace Trailblazor.Search.Logging;

internal sealed class CallbackLoggingHandler(
    IServiceProvider _serviceProvider,
    ISearchEngineOptionsProvider _searchEngineOptionsProvider) : ICallbackLoggingHandler
{
    private ILogger<CallbackLoggingHandler>? _logger;
    private ILogger<CallbackLoggingHandler>? Logger => _logger ??= _serviceProvider.GetService<ILogger<CallbackLoggingHandler>>();

    public void LogReportFailed(Guid handlerId, Exception? exception = null)
    {
        try
        {
            if (Logger == null)
                return;

            var handlerConfiguration = GetHandlerConfiguration(handlerId);
            if (exception == null)
                Logger.LogError($"Search request handler '{handlerConfiguration.HandlerType}' reported back as failed.");
            else
                Logger.LogError(exception, $"Search request handler '{handlerConfiguration.HandlerType}' reported back an exception: {exception}");
        }
        catch
        {
        }
    }

    public void LogReportFinished(Guid handlerId)
    {
        try
        {
            if (Logger == null)
                return;

            var handlerConfiguration = GetHandlerConfiguration(handlerId);
            Logger.LogInformation($"Search request handler '{handlerConfiguration.HandlerType}' reported back as finished.");
        }
        catch
        {
        }
    }

    public void LogReportResults(Guid handlerId, IReadOnlyList<ISearchResult> results)
    {
        try
        {
            if (Logger == null)
                return;

            var handlerConfiguration = GetHandlerConfiguration(handlerId);
            Logger.LogInformation($"Search request handler '{handlerConfiguration.HandlerType}' reported back {results.Count} results.");
        }
        catch
        {
        }
    }

    private ISearchRequestHandlerConfiguration GetHandlerConfiguration(Guid handlerId)
    {
        return _searchEngineOptionsProvider.GetOptions().OperationConfigurations.SelectMany(o => o.ThreadConfigurations).SelectMany(o => o.HandlerConfigurations).Single(h => h.HandlerId == handlerId);
    }
}
