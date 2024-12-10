namespace Trailblazor.Search.Criterias.Workers;

/// <summary>
/// Service handles filtering items with a given <see cref="StringSearchCriteria"/>.
/// </summary>
public interface IStringSearchCriteriaWorker
{
    /// <summary>
    /// Filters the given <paramref name="items"/> using the given <paramref name="searchCriteria"/>.
    /// </summary>
    /// <typeparam name="TItem">Type of items being searched.</typeparam>
    /// <param name="items">Items being searched.</param>
    /// <param name="searchCriteria">String search criteria being used to filter.</param>
    /// <returns>Filtered <see cref="IQueryable{T}"/>.</returns>
    public IQueryable<TItem> SearchItems<TItem>(IEnumerable<TItem> items, StringSearchCriteria searchCriteria);
}
