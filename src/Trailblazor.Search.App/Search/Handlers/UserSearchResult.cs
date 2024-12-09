using Trailblazor.Search.App.Domain;

namespace Trailblazor.Search.App.Search.Handlers;

public sealed class UserSearchResult(User user) : SearchResult
{
    public User User { get; } = user;
    public override object Key => User.Id;
}
