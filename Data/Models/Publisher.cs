using System;
namespace MyBooks.Data.Models
{
	public class Publisher
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// Relationship
		public List<Book> Books { get; set; }
	}
}

