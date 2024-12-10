using System.Collections;
using System.Reflection;

namespace Trailblazor.Search.Criterias.Workers;

internal sealed class StringSearchCriteriaWorker : IStringSearchCriteriaWorker
{
    public IQueryable<TItem> SearchItems<TItem>(IEnumerable<TItem> items, StringSearchCriteria searchCriteria)
    {
        if (string.IsNullOrWhiteSpace(searchCriteria.Value))
            return items.AsQueryable();

        var searchTerm = searchCriteria.CaseSensitive == true
            ? searchCriteria.Value
            : searchCriteria.Value.ToLowerInvariant();

        return items.Where(item => item != null && ItemMatches(item, searchTerm, searchCriteria.CaseSensitive, searchCriteria.WholeTerm)).AsQueryable();
    }

    private bool ItemMatches(object item, string searchTerm, bool caseSensitive, bool wholeTerm)
    {
        var properties = GetSearchableProperties(item.GetType());

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(item);
            if (property.PropertyType.IsValueType || propertyValue is string)
            {
                var value = propertyValue is string stringValue ? stringValue : propertyValue?.ToString();
                if (MatchesSearchTerm(value, searchTerm, caseSensitive, wholeTerm))
                    return true;
            }
            else if (propertyValue is IEnumerable enumerable)
            {
                foreach (var element in enumerable)
                {
                    if (ItemMatches(element, searchTerm, caseSensitive, wholeTerm))
                        return true;
                }
            }
            else if (propertyValue != null)
            {
                if (ItemMatches(propertyValue, searchTerm, caseSensitive, wholeTerm))
                    return true;
            }
        }

        return false;
    }

    private bool MatchesSearchTerm(string? value, string searchTerm, bool caseSensitive, bool wholeTerm)
    {
        if (value == null)
            return false;

        if (wholeTerm == true)
        {
            return caseSensitive == true
                ? value.Equals(searchTerm)
                : value.Equals(searchTerm, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            return caseSensitive == true
                ? value.Contains(searchTerm)
                : value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }
    }

    private List<PropertyInfo> GetSearchableProperties(Type type)
    {
        return type.GetProperties()
            .Where(prop => prop.GetCustomAttribute<IncludeOnStringSearchAttribute>() != null)
            .ToList();
    }
}