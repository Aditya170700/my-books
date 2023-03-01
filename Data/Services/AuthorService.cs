using System;
using MyBooks.Data.Models;
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

        public List<Author> GetAuthors() => _dbContext.Authors.ToList();

        public Author GetAuthorById(int Id) => _dbContext.Authors.FirstOrDefault(b => b.Id == Id);

        public Author UpdateAuthor(int Id, AuthorViews avm)
        {
            var _author = _dbContext.Authors.FirstOrDefault(b => b.Id == Id);

            if (_author != null)
            {
                _author.FullName = avm.FullName;

                _dbContext.SaveChanges();
            }

            return _author;
        }

        public void DeleteAuthor(int Id)
        {
            var _author = _dbContext.Authors.FirstOrDefault(b => b.Id == Id);

            if (_author != null)
            {
                _dbContext.Authors.Remove(_author);
                _dbContext.SaveChanges();
            }
        }
    }
}

