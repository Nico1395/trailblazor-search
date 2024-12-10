namespace Trailblazor.Search.Logging;

public interface ICallbackLoggingHandler
{
    public void LogReportResults(Guid handlerId, IReadOnlyList<ISearchResult> results);
    public void LogReportFinished(Guid handlerId);
    public void LogReportFailed(Guid handlerId, Exception? exception = null);
}
