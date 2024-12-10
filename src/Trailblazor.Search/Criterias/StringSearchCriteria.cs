namespace Trailblazor.Search.Criterias;

public record StringSearchCriteria
{
    public string? Value { get; set; }
    public bool CaseSensitive { get; set; }
    public bool WholeTerm { get; set; }
}
