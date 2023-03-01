using System;
namespace MyBooks.Data.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string FullName { get; set; }

		// Relationship
		public List<BookAuthor> BookAuthors { get; set; }
	}
}

