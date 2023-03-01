using System;
namespace MyBooks.Data.Views
{
	public class BookViews
	{
        public string Title { get; set; }
        public string description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public int Rate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string CoverUrl { get; set; }
    }
}

