using System;
using MyBooks.Data.Queries;

namespace MyBooks.Data.Views
{
	public class PaginatedViews<T>
	{
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public string FirstPageUrl { get; private set; }
        public string PreviousPageUrl { get; private set; }
        public string NextPageUrl { get; private set; }
        public string LastPageUrl { get; private set; }
        public List<PaginateLink> Links { get; set; }
        public List<T> Items { get; private set; }

        public PaginatedViews(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            PreviousPageUrl = pageNumber > 1 ? $"/api/books?pageNumber={pageNumber - 1}&pageSize={pageSize}" : null;
            NextPageUrl = pageNumber < TotalPages ? $"/api/books?pageNumber={pageNumber + 1}&pageSize={pageSize}" : null;
            FirstPageUrl = pageNumber > 1 ? "/api/books?pageNumber=1&pageSize={pageSize}" : null;
            LastPageUrl = pageNumber < TotalPages ? $"/api/books?pageNumber={TotalPages}&pageSize={pageSize}" : null;
            Items = items;

            var _links = new List<PaginateLink>();

            for (int i = 1; i <= TotalPages; i++)
            {
                _links.Add(new PaginateLink
                {
                    Number = i,
                    Url = $"/api/books?pageNumber={i}&pageSize={pageSize}",
                    Active = i == pageNumber,
                });
            }

            Links = _links;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static PaginatedViews<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedViews<T>(items, count, pageNumber, pageSize);
        }
    }

    public class PaginateLink
    {
        public int Number { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
    }
}

