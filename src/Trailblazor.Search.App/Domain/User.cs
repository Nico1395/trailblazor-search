using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Domain;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [IncludeOnSearch]
    public string FirstName { get; set; }

    [IncludeOnSearch]
    public string LastName { get; set; }

    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
