using System;
using System.Threading;
using MyBooks.Data.Models;
using MyBooks.Data.Queries;
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
            var _transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var _book = new Book()
                {
                    Title = bvm.Title,
                    description = bvm.description,
                    IsRead = bvm.IsRead,
                    ReadAt = bvm.IsRead ? bvm.ReadAt.Value : null,
                    Rate = bvm.IsRead ? bvm.Rate : 0,
                    Genre = bvm.Genre,
                    CoverUrl = bvm.CoverUrl,
                    CreatedAt = DateTime.Now,
                    PublisherId = bvm.PublisherId,
                };

                _dbContext.Add(_book);
                _dbContext.SaveChanges();

                var _bookAuthors = new List<BookAuthor>();

                foreach (var id in bvm.AuthorIds)
                {
                    _dbContext.Add(new BookAuthor()
                    {
                        BookId = _book.Id,
                        AuthorId = id,
                    });
                }

                _dbContext.SaveChanges();

                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public List<Book> GetBooks(string field, string sort)
        {
            IQueryable<Book> query = _dbContext.Books;

            if (!string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(sort))
            {
                query = query.OrderByProperty(field, sort.ToLower() == "desc");
            }

            return query.ToList();
        }

        public BookPublisherAuthorsViews GetBookById(int Id)
        {
            var _book = _dbContext.Books.Where(b => b.Id == Id)
                .Select(book => new BookPublisherAuthorsViews()
                {
                    Title = book.Title,
                    description = book.description,
                    IsRead = book.IsRead,
                    ReadAt = book.IsRead ? book.ReadAt.Value : null,
                    Rate = book.IsRead ? book.Rate : 0,
                    Genre = book.Genre,
                    CoverUrl = book.CoverUrl,
                    PublisherName = book.Publisher.Name,
                    AuthorNames = book.BookAuthors.Select(ba => ba.Author.FullName).ToList(),
                })
                .FirstOrDefault();

            if (_book == null)
            {
                throw new Exception($"Buuk with id : {Id} does not exists");
            }

            return _book;
        }

        public Book UpdateBook(int Id, BookViews bvm)
        {
            var _book = _dbContext.Books.FirstOrDefault(b => b.Id == Id);

            if (_book == null)
            {
                throw new Exception($"Buuk with id : {Id} does not exists");
            }

            _book.Title = bvm.Title;
            _book.description = bvm.description;
            _book.IsRead = bvm.IsRead;
            _book.ReadAt = bvm.IsRead ? bvm.ReadAt.Value : null;
            _book.Rate = bvm.IsRead ? bvm.Rate : 0;
            _book.Genre = bvm.Genre;
            _book.CoverUrl = bvm.CoverUrl;

            _dbContext.SaveChanges();

            return _book;
        }

        public void DeleteBook(int Id)
        {
            var _book = _dbContext.Books.FirstOrDefault(b => b.Id == Id);

            if (_book == null)
            {
                throw new Exception($"Buuk with id : {Id} does not exists");
            }

            _dbContext.Books.Remove(_book);
            _dbContext.SaveChanges();
        }
    }
}

