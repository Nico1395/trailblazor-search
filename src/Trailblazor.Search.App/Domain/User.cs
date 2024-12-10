using Trailblazor.Search.Criteria.Workers;

namespace Trailblazor.Search.App.Domain;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [IncludeOnStringSearch]
    public string FirstName { get; set; }

    [IncludeOnStringSearch]
    public string LastName { get; set; }

    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
