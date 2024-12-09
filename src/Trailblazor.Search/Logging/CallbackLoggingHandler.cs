namespace Trailblazor.Search.Logging;

internal sealed class CallbackLoggingHandler : ICallbackLoggingHandler
{
    public void LogReportFailed(SearchRequestHandlerMetadata handlerMetadata, Exception? exception = null)
    {
    }

    public void LogReportFinished(SearchRequestHandlerMetadata handlerMetadata)
    {
    }

    public void LogReportResults(SearchRequestHandlerMetadata handlerMetadata, IReadOnlyList<ISearchResult> results)
    {
    }
}
