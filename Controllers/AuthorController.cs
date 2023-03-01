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
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorViews avm)
        {
            _authorService.AddAuthor(avm);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            return Ok(_authorService.GetAuthors());
        }

        [HttpGet("{Id}")]
        public IActionResult GetAuthor(int Id)
        {
            return Ok(_authorService.GetAuthorById(Id));
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateAuthor(int Id, [FromBody] AuthorViews avm)
        {
            return Ok(_authorService.UpdateAuthor(Id, avm));
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteAuthor(int Id)
        {
            _authorService.DeleteAuthor(Id);
            return Ok();
        }
    }
}

