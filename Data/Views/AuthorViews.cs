using System;
namespace MyBooks.Data.Views
{
	public class AuthorViews
    {
        public string FullName { get; set; }
    }

    public class AuthorBooksViews
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}

