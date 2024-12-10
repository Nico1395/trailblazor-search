namespace Trailblazor.Search.Criteria;

public record EnumSearchCriteria<TEnum>
    where TEnum : Enum
{
    public TEnum? Value { get; set; }
}
