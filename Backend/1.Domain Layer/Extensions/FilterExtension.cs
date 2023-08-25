using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DomainLayer.Extensions;

public static class FilterExtension
{
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        if (condition)
            return source.Where(predicate);

        return source;
    }

    public static IOrderedQueryable<TSource> Sort<TSource>(this IQueryable<TSource> source, bool ascending, string sortingProperty)
    {
        if (ascending)
            return source.OrderBy(item => item.GetReflectedPropertyValue(sortingProperty));
        else
            return source.OrderByDescending(item => item.GetReflectedPropertyValue(sortingProperty));
    }

    private static object GetReflectedPropertyValue(this object subject, string field)
    {
        return subject.GetType().GetProperty(field).GetValue(subject, null);
    }
    public static IQueryable<TEntity> ApplyOrderBy<TEntity>(
       this IQueryable<TEntity> query, string? orderBy, string orderDirection)
    {
        if (orderBy is null) return query;

        query = orderDirection == "Asc"
            ? query.OrderBy(p => EF.Property<TEntity>(p!, orderBy))
            : query.OrderByDescending(p => EF.Property<TEntity>(p!, orderBy));

        return query;
    }
}