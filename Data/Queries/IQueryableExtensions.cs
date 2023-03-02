using System;
using System.Linq.Expressions;

namespace MyBooks.Data.Queries
{
	public static class IQueryableExtensions
	{
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool descending = false)
        {
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentException($"Property {propertyName} not found in {type}");

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var methodName = descending ? "OrderByDescending" : "OrderBy";

            return source.Provider.CreateQuery<T>(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new Type[] { type, property.PropertyType },
                    source.Expression,
                    Expression.Quote(orderByExp)
                )
            );
        }

    }
}

