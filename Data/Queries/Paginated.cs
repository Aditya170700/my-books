using System;
using MyBooks.Data.Views;

namespace MyBooks.Data.Queries
{
	public static class Paginated
	{
        public static PaginatedViews<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedViews<T>(items, count, pageNumber, pageSize);
        }
    }
}

