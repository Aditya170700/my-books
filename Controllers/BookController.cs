using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.Views;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookViews bvm)
        {
            _bookService.AddBook(bvm);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("{Id}")]
        public IActionResult GetBook(int Id)
        {
            return Ok(_bookService.GetBookById(Id));
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateBook(int Id, [FromBody] BookViews bvm)
        {
            return Ok(_bookService.UpdateBook(Id, bvm));
        }
    }
}

