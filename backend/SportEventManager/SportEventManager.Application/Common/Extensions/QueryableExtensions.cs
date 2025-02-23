using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace SportEventManager.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyDynamicFiltering<T>(
            this IQueryable<T> query,
            string? searchTerm,
            string[] properties)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? filterExpression = null;

            foreach (var property in properties)
            {
                var propertyInfo = typeof(T).GetProperty(
                    property,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                );

                if (propertyInfo?.PropertyType != typeof(string))
                    continue;

                var propertyAccess = Expression.Property(parameter, propertyInfo);
                var searchTermExpression = Expression.Constant($"%{searchTerm}%", typeof(string));

                var efFunctions = Expression.Constant(EF.Functions);
                var likeMethod = typeof(DbFunctionsExtensions).GetMethod(
                    nameof(DbFunctionsExtensions.Like),
                    new[] { typeof(DbFunctions), typeof(string), typeof(string) }
                ) ?? throw new InvalidOperationException("EF.Functions.Like method not found");

                var likeExpression = Expression.Call(
                    likeMethod,
                    efFunctions,
                    propertyAccess,
                    searchTermExpression
                );

                filterExpression = filterExpression == null
                    ? likeExpression
                    : Expression.OrElse(filterExpression, likeExpression);
            }

            if (filterExpression == null)
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
            return query.Where(lambda);
        }

        public static IQueryable<T> ApplyDynamicSorting<T>(
            this IQueryable<T> query,
            string? sortColumn,
            bool sortDescending)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                return query;

            var propertyInfo = typeof(T).GetProperty(
                sortColumn,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            ) ?? throw new ArgumentException($"Invalid sort column '{sortColumn}'");

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, propertyInfo);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = sortDescending ? "OrderByDescending" : "OrderBy";
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

            return (IQueryable<T>)method.Invoke(null, new object[] { query, orderByExpression })!;
        }
    }
}
