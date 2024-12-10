namespace Trailblazor.Search.Requests.Criteria;

public record ListSearchCriteria<TItem, TKey>
{
    public List<TKey> ItemKeys { get; set; } = [];
    public ListSearchCriteriaMode Mode { get; set; } = ListSearchCriteriaMode.Include;
}
