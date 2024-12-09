using System.Collections;
using System.Reflection;

namespace Trailblazor.Search.Workers;

internal sealed class SearchWorker : ISearchWorker
{
    public IQueryable<TItem> SearchItems<TItem>(IEnumerable<TItem> items, ISearchWorkerDescriptor descriptor)
    {
        if (descriptor is not SearchTermSearchWorkerDescriptor searchDescriptor)
            return items.AsQueryable();

        if (string.IsNullOrWhiteSpace(searchDescriptor.SearchTerm))
            return items.AsQueryable();

        var searchTerm = searchDescriptor.CaseSensitive == true
            ? searchDescriptor.SearchTerm
            : searchDescriptor.SearchTerm.ToLowerInvariant();

        return items.Where(item => item != null && ItemMatches(item, searchTerm, searchDescriptor.CaseSensitive, searchDescriptor.WholeTerm)).AsQueryable();
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
            .Where(prop => prop.GetCustomAttribute<IncludeOnSearchAttribute>() != null)
            .ToList();
    }
}