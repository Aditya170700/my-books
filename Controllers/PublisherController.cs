using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.Views;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    public class PublisherController : Controller
    {
        private readonly PublisherService _publisherService;

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost]
        public IActionResult AddPublisher([FromBody] PublisherViews pvm)
        {
            _publisherService.AddPublisher(pvm);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPublishers()
        {
            return Ok(_publisherService.GetPublishers());
        }

        [HttpGet("{Id}")]
        public IActionResult GetPublisher(int Id)
        {
            return Ok(_publisherService.GetPublisherById(Id));
        }

        [HttpPut("{Id}")]
        public IActionResult UpdatePublisher(int Id, [FromBody] PublisherViews pvm)
        {
            return Ok(_publisherService.UpdatePublisher(Id, pvm));
        }

        [HttpDelete("{Id}")]
        public IActionResult DeletePublisher(int Id)
        {
            _publisherService.DeletePublisher(Id);
            return Ok();
        }
    }
}

