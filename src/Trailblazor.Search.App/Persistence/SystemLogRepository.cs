using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Persistence;

internal sealed class SystemLogRepository : ISystemLogRepository
{
    private List<SystemLog>? _systemLogs;

    public Task<List<SystemLog>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_systemLogs ??= GenerateSystemLogs(200));
    }

    private List<SystemLog> GenerateSystemLogs(int count)
    {
        var logs = new List<SystemLog>();
        for (int i = 0; i < count; i++)
        {
            logs.Add(new SystemLog
            {
                Message = $"Log message {i + 1}",
                Created = DateTime.UtcNow.AddMinutes(-i),
            });
        }
        return logs;
    }
}
