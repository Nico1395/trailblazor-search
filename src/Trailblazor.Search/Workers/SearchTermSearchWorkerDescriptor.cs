namespace Trailblazor.Search.Workers;

public record SearchTermSearchWorkerDescriptor : ISearchWorkerDescriptor
{
    public string? SearchTerm { get; set; }
    public bool CaseSensitive { get; set; }
    public bool WholeTerm { get; set; }
}
