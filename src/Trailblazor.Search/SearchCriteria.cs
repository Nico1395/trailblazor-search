namespace Trailblazor.Search;

public record SearchCriteria<T>
    where T : struct, IComparable<T>
{
    public SearchCriteria()
    {
        Mode = SearchCriteriaMode.Equals;
    }

    public T? Value { get; set; }
    public SearchCriteriaMode Mode { get; set; }

    public override string ToString()
    {
        if (!Value.HasValue)
            return "null";

        return $"{Mode}: {Value}";
    }
}
