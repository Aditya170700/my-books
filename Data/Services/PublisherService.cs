using System;
using MyBooks.Data.Models;
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

        public void AddPublisher(PublisherViews pvm)
        {
            var _publisher = new Publisher()
            {
                Name = pvm.Name,
            };

            _dbContext.Add(_publisher);
            _dbContext.SaveChanges();
        }

        public List<Publisher> GetPublishers() => _dbContext.Publishers.ToList();

        public Publisher GetPublisherById(int Id) => _dbContext.Publishers.FirstOrDefault(b => b.Id == Id);

        public Publisher UpdatePublisher(int Id, PublisherViews pvm)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(b => b.Id == Id);

            if (_publisher != null)
            {
                _publisher.Name = pvm.Name;

                _dbContext.SaveChanges();
            }

            return _publisher;
        }

        public void DeletePublisher(int Id)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(b => b.Id == Id);

            if (_publisher != null)
            {
                _dbContext.Publishers.Remove(_publisher);
                _dbContext.SaveChanges();
            }
        }
    }
}

