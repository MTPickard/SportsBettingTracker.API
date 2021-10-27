using _01_SportsBetting.Data;
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
        public IHttpActionResult Post(_02_BookModel book)
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
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            _02_BookService bookService = CreateBookService();
            var books = bookService.GetBookByUserId();
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetByBookId(int id)
        {
            _02_BookService bookService = CreateBookService();
            var book = bookService.GetBookByBookId(id);
            return Ok(book);
        }

        [HttpPut]
        public IHttpActionResult Put(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = CreateBookService();

            if (!service.UpdateBook(book))
            {
                return InternalServerError();
            }
            return Ok();
        }

        // D - Delete One By GameId
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateBookService();

            if (service.DeleteBook(id))
            {
                return InternalServerError();
            }
            return Ok();
        }

        private _02_BookService CreateBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bookService = new _02_BookService(userId);
            return bookService;
        }
    }
}
