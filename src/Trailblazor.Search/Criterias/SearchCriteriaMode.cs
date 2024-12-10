namespace Trailblazor.Search.Criterias;

/// <summary>
/// Mode of a comparable <see cref="SearchCriteria{T}"/>.
/// </summary>
public enum SearchCriteriaMode
{
    /// <summary>
    /// Equals the critera value.
    /// </summary>
    Equals,

    /// <summary>
    /// Is greater than the critera value.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Is greater than or equals the critera value.
    /// </summary>
    GreaterThanEquals,

    /// <summary>
    /// Is less than the critera value.
    /// </summary>
    LessThan,

    /// <summary>
    /// Is less than or equals the critera value.
    /// </summary>
    LessThanEquals,
}
