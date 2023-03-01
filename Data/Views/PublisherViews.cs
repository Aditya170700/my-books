using System;
namespace MyBooks.Data.Views
{
	public class PublisherViews
    {
        public string Name { get; set; }
    }

    public class PublisherBookAuthorsViews
    {
        public string Name { get; set; }
        public List<BookAuthorsView> BookAuthors { get; set; }
    }

    public class BookAuthorsView
    {
        public string Title { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}

