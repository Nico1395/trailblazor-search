using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Domain;

public class SystemLog
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [IncludeOnSearch]
    public string Message { get; set; }

    public DateTime Created { get; set; }
}
