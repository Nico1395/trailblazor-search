using System.Linq.Expressions;
using Trailblazor.Search.Criteria;

namespace Trailblazor.Search.Criteria.Extensions;

public static class CriteriaEnumerableExtensions
{
    public static IQueryable<T> WhereMatchesCriteria<T, TValue>(this IQueryable<T> queryable, Expression<Func<T, TValue>> propertySelector, SearchCriteria<TValue> criteria)
        where TValue : struct, IComparable<TValue>
    {
        if (!criteria.Value.HasValue)
            return queryable;

        var property = propertySelector.Body;
        var criteriaValueConstant = Expression.Constant(criteria.Value.Value, typeof(TValue));
        var comparison = criteria.Mode switch
        {
            SearchCriteriaMode.Equals => Expression.Equal(property, criteriaValueConstant),
            SearchCriteriaMode.GreaterThan => Expression.GreaterThan(property, criteriaValueConstant),
            SearchCriteriaMode.GreaterThanEquals => Expression.GreaterThanOrEqual(property, criteriaValueConstant),
            SearchCriteriaMode.LessThan => Expression.LessThan(property, criteriaValueConstant),
            SearchCriteriaMode.LessThanEquals => Expression.LessThanOrEqual(property, criteriaValueConstant),
            _ => throw new ArgumentOutOfRangeException(nameof(criteria.Mode), "Invalid search criteria mode.")
        };

        var lambda = Expression.Lambda<Func<T, bool>>(comparison, propertySelector.Parameters[0]);
        return queryable.Where(lambda);
    }

    public static IQueryable<T> WhereMatchesCriteria<T>(this IQueryable<T> queryable, Expression<Func<T, string>> propertySelector, StringSearchCriteria criteria)
    {
        if (string.IsNullOrWhiteSpace(criteria.Value))
            return queryable;

        var property = propertySelector.Body;
        var criteriaValueConstant = Expression.Constant(criteria.CaseSensitive ? criteria.Value : criteria.Value.ToLowerInvariant(), typeof(string));

        if (!criteria.CaseSensitive)
            property = Expression.Call(property, typeof(string).GetMethod("ToLower", Type.EmptyTypes) ?? throw new InvalidProgramException());

        Expression comparison = criteria.WholeTerm
            ? Expression.Equal(property, criteriaValueConstant)
            : Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }) ?? throw new InvalidProgramException(), criteriaValueConstant);

        var lambda = Expression.Lambda<Func<T, bool>>(comparison, propertySelector.Parameters[0]);
        return queryable.Where(lambda);
    }

    // TODO -> An extension for every criteria
}
