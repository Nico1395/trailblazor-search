using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Persistence;

internal sealed class UserRepository : IUserRepository
{
    private List<User>? _users;

    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_users ??= GenerateUsers(20));
    }

    private List<User> GenerateUsers(int count)
    {
        var users = new List<User>();
        for (int i = 0; i < count; i++)
        {
            users.Add(new User
            {
                FirstName = $"FirstName_{i + 1}",
                LastName = $"LastName_{i + 1}",
                Created = DateTime.UtcNow,
                LastChanged = DateTime.UtcNow
            });
        }
        return users;
    }
}
