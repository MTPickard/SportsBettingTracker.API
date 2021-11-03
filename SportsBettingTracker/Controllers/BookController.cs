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
    [Authorize]
    public class BookController : ApiController
    {
        // C POST PostBook
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

        // R GET GetBooksbyUserId
        public IHttpActionResult GetBooksByUserId()
        {
            BookService bookService = CreateBookService();
            var books = bookService.ViewBooksByUserId();
            return Ok(books);
        }

        // R GET GetBookByBookId
        public IHttpActionResult GetBookByBookId(int id)
        {
            BookService bookService = CreateBookService();
            var book = bookService.ViewBookByBookId(id);
            return Ok(book);
        }

        // U PUT PutBookByBookId
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

        // D DELETE DeleteBookByBookId
        public IHttpActionResult DeleteBookByBookId(int id)
        {
            var service = CreateBookService();

            if (!service.RemoveBookByBookId(id))
            {
                return InternalServerError();
            }
            return Ok("Book deleted.");
        }

        // "Helper Method"
        private BookService CreateBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bookService = new BookService(userId);
            return bookService;
        }
    }
}
