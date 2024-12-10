using Trailblazor.Search.Criteria.Workers;

namespace Trailblazor.Search.App.Domain;

public class SystemLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [IncludeOnStringSearch]
    public string Message { get; set; }

    public DateTime Created { get; set; }
}
