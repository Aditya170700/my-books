using System;
using MyBooks.Data.Models;
using MyBooks.Data.Queries;
using MyBooks.Data.Views;

namespace MyBooks.Data.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _dbContext;

        public AuthorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddAuthor(AuthorViews avm)
        {
            var _author = new Author()
            {
                FullName = avm.FullName,
            };

            _dbContext.Add(_author);
            _dbContext.SaveChanges();
        }

        public PaginatedViews<Author> GetAuthors(string field, string sort, string search, int pageNumber, int pageSize)
        {
            IQueryable<Author> query = _dbContext.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(field))
            {
                if (!string.IsNullOrEmpty(sort))
                {
                    query = query.OrderByProperty(field, sort.ToLower() == "desc");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.SearchByProperty(field, search);
                }
            }

            PaginatedViews<Author> results = query.Paginate(pageNumber, pageSize);

            return results;
        }

        public AuthorBooksViews GetAuthorById(int Id)
        {
            var _author = _dbContext.Authors.Where(a => a.Id == Id)
                .Select(a => new AuthorBooksViews()
                {
                    FullName = a.FullName,
                    BookTitles = a.BookAuthors.Select(ba => ba.Book.Title).ToList()
                })
                .FirstOrDefault();

            if (_author == null)
            {
                throw new Exception($"Author with id : {Id} does not exists");
            }

            return _author;
        }

        public Author UpdateAuthor(int Id, AuthorViews avm)
        {
            var _author = _dbContext.Authors.FirstOrDefault(b => b.Id == Id);

            if (_author == null)
            {
                throw new Exception($"Author with id : {Id} does not exists");
            }

            _author.FullName = avm.FullName;

            _dbContext.SaveChanges();

            return _author;
        }

        public void DeleteAuthor(int Id)
        {
            var _author = _dbContext.Authors.FirstOrDefault(b => b.Id == Id);

            if (_author == null)
            {
                throw new Exception($"Author with id : {Id} does not exists");
            }

            _dbContext.Authors.Remove(_author);
            _dbContext.SaveChanges();
        }
    }
}

