using Trailblazor.Search.Criteria.Workers;

namespace Trailblazor.Search.App.Domain;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [IncludeOnStringSearch]
    public string Name { get; set; }

    [IncludeOnStringSearch]
    public string Description { get; set; }

    public int InStock { get; set; }
    public int Sold { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChanged { get; set; }
}
