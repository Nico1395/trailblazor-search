using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Persistence;

public interface ISystemLogRepository
{
    public Task<List<SystemLog>> GetAllAsync(CancellationToken cancellationToken);
}
