using System;
using System.Threading;
using MyBooks.Data.Models;
using MyBooks.Data.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyBooks.Data.Services
{
	public class BookService
	{
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext dbContext)
		{
            _dbContext = dbContext;
        }

        public void AddBook(BookViews bvm)
        {
            var _book = new Book()
            {
                Title = bvm.Title,
                description = bvm.description,
                IsRead = bvm.IsRead,
                ReadAt = bvm.IsRead ? bvm.ReadAt.Value : null,
                Rate = bvm.IsRead ? bvm.Rate : 0,
                Author = bvm.Author,
                Genre = bvm.Genre,
                CoverUrl = bvm.CoverUrl,
                CreatedAt = DateTime.Now
            };

            _dbContext.Add(_book);
            _dbContext.SaveChanges();
        }

        public List<Book> GetBooks() => _dbContext.Books.ToList();

        public Book GetBookById(int Id) => _dbContext.Books.FirstOrDefault(b => b.Id == Id);

        public Book UpdateBook(int Id, BookViews bvm)
        {
            var _book = _dbContext.Books.FirstOrDefault(b => b.Id == Id);

            if (_book != null)
            {
                _book.Title = bvm.Title;
                _book.description = bvm.description;
                _book.IsRead = bvm.IsRead;
                _book.ReadAt = bvm.IsRead ? bvm.ReadAt.Value : null;
                _book.Rate = bvm.IsRead ? bvm.Rate : 0;
                _book.Author = bvm.Author;
                _book.Genre = bvm.Genre;
                _book.CoverUrl = bvm.CoverUrl;

                _dbContext.SaveChanges();
            }

            return _book;
        }
    }
}

