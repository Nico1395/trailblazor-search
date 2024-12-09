using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Persistence;

public interface IUserRepository
{
    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
}
