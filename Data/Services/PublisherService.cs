using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyBooks.Data.Models;
using MyBooks.Data.Queries;
using MyBooks.Data.Views;

namespace MyBooks.Data.Services
{
	public class PublisherService
    {
        private readonly AppDbContext _dbContext;

        public PublisherService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Publisher AddPublisher(PublisherViews pvm)
        {
            var _publisher = new Publisher()
            {
                Name = pvm.Name,
            };

            _dbContext.Add(_publisher);
            _dbContext.SaveChanges();

            return _publisher;
        }

        public PaginatedViews<Publisher> GetPublishers(string field, string sort, string search, int pageNumber, int pageSize)
        {
            IQueryable<Publisher> query = _dbContext.Publishers;

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

            PaginatedViews<Publisher> results = query.Paginate(pageNumber, pageSize);

            return results;
        }

        public PublisherBookAuthorsViews GetPublisherById(int Id)
        {
            var _publisher = _dbContext.Publishers.Where(p => p.Id == Id)
                .Select(p => new PublisherBookAuthorsViews() {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(b => new BookAuthorsView() {
                        Title = b.Title,
                        BookAuthors = b.BookAuthors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList(),
                })
                .FirstOrDefault();

            if (_publisher == null)
            {
                throw new Exception($"Publisher with id : {Id} does not exists");
            }

            return _publisher;
        }

        public Publisher UpdatePublisher(int Id, PublisherViews pvm)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(b => b.Id == Id);

            if (_publisher == null)
            {
                throw new Exception($"Publisher with id : {Id} does not exists");
            }

            _publisher.Name = pvm.Name;
            _dbContext.SaveChanges();

            return _publisher;
        }

        public void DeletePublisher(int Id)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(b => b.Id == Id);

            if (_publisher == null)
            {
                throw new Exception($"Publisher with id : {Id} does not exists");
            }

            _dbContext.Publishers.Remove(_publisher);
            _dbContext.SaveChanges();
        }
    }
}

