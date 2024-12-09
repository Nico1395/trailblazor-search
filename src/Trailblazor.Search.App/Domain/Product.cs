using Trailblazor.Search.Workers;

namespace Trailblazor.Search.App.Domain;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [IncludeOnSearch]
    public string Name { get; set; }

    [IncludeOnSearch]
    public string Description { get; set; }

    public int InStock { get; set; }
    public int Sold { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
