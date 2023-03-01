using System;
namespace MyBooks.Data.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string description { get; set; }
		public bool IsRead { get; set; }
		public DateTime? ReadAt { get; set; }
		public int Rate { get; set; }
		public string Genre { get; set; }
        public string CoverUrl { get; set; }
		public DateTime CreatedAt { get; set; }

		// Relationship
		public int PublisherId { get; set; }
		public Publisher Publisher { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}

