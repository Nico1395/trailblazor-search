namespace Trailblazor.Search;

public record EnumSearchCriteria<TEnum>
    where TEnum : Enum
{
    public TEnum? Value { get; set; }
}
