using _02_SportsBetting.Models;
using _03_SportsBetting.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SportsBettingTracker.Controllers
{
    public class BookController : ApiController
    {
        [HttpPost]
        public IHttpActionResult PostBook(BookCreate book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateBookService();

            if (!service.CreateBook(book))
            {
                return InternalServerError();
            }
            return Ok("Book has been added.");
        }

        [HttpGet]
        public IHttpActionResult GetBooksByUserId()
        {
            BookService bookService = CreateBookService();
            var books = bookService.ViewBooksByUserId();
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetBookByBookId(int id)
        {
            BookService bookService = CreateBookService();
            var book = bookService.ViewBookByBookId(id);
            return Ok(book);
        }

        [HttpPut]
        public IHttpActionResult PutBookByBookId(BookEdit book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateBookService();

            if (!service.UpdateBookByBookId(book))
            {
                return InternalServerError();
            }
            return Ok("Book updated.");
        }

        [HttpDelete]
        public IHttpActionResult DeleteBookByBookId(int id)
        {
            var service = CreateBookService();

            if (!service.RemoveBookByBookId(id))
            {
                return InternalServerError();
            }
            return Ok("Book deleted.");
        }

        private BookService CreateBookService()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var bookService = new BookService(userId);
            return bookService;
        }
    }
}
