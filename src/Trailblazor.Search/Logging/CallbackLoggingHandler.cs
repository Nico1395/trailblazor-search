namespace Trailblazor.Search.Logging;

internal sealed class CallbackLoggingHandler : ICallbackLoggingHandler
{
    public void LogReportFailed(Guid handlerId, Exception? exception = null)
    {
    }

    public void LogReportFinished(Guid handlerId)
    {
    }

    public void LogReportResults(Guid handlerId, IReadOnlyList<ISearchResult> results)
    {
    }
}
