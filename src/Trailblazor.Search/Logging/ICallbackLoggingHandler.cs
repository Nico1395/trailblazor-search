namespace Trailblazor.Search.Logging;

public interface ICallbackLoggingHandler
{
    public void LogReportResults(SearchRequestHandlerMetadata handlerMetadata, IReadOnlyList<ISearchResult> results);
    public void LogReportFinished(SearchRequestHandlerMetadata handlerMetadata);
    public void LogReportFailed(SearchRequestHandlerMetadata handlerMetadata, Exception? exception = null);
}
