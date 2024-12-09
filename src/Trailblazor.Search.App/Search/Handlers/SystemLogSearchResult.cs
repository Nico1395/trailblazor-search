using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Search.Handlers;

public sealed class SystemLogSearchResult(SystemLog systemLog) : SearchResult
{
    public SystemLog SystemLog { get; } = systemLog;
    public override object Key => SystemLog.Id;
}
