namespace Trailblazor.Search.Requests.Criteria;

/// <summary>
/// Search criteria of a <see cref="ISearchRequest"/> with a <see cref="SearchCriteriaMode"/> for <see cref="IComparable{T}"/> types.
/// </summary>
/// <typeparam name="T">Type of value.</typeparam>
public record SearchCriteria<T>
    where T : struct, IComparable<T>
{
    /// <summary>
    /// Value of the criteria.
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// Mode of the criteria.
    /// </summary>
    public SearchCriteriaMode Mode { get; set; } = SearchCriteriaMode.Equals;

    /// <inheritdoc/>
    public override string ToString()
    {
        if (!Value.HasValue)
            return "null";

        return $"{Mode}: {Value}";
    }
}
