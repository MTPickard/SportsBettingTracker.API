using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using SportsBettingTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class _02_BookService
    {
        private readonly Guid _userId;

        public _02_BookService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBook(_02_BookModel book)
        {
            var newBook = new Book()
            {
                Name = book.Name,
                Balance = book.Balance,
                BookReference = book.BookReference,
                CreatedUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(newBook);
                return ctx.SaveChanges() == 1;
            }
        }

        public Book GetBookByBookId(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var book =
                    ctx
                    .Books
                    .Where(b => b.UserId == _userId)
                    .Single(b=> b.BookId == bookId);
                return
                    new Book
                    {
                        BookId = book.BookId,
                        UserId = book.UserId,
                        _transactions = book._transactions,
                        _bets = book._bets,
                        Name = book.Name,
                        Balance = book.Balance,
                        BookReference = book.BookReference,
                        CreatedUtc = book.CreatedUtc,
                        ModifiedUtc = book.ModifiedUtc
                    };
            }
        }

        public IQueryable<Book> GetBookByUserId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var book =
                    ctx
                    .Books
                    .Where(b => b.UserId == _userId);
                return book;
            }
        }

        public bool UpdateBook(Book book)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Books
                    .Where(e => e.UserId == _userId)
                    .Single(e => e.BookId == book.BookId);

                entity.Name = book.Name;
                entity.Balance = book.Balance;
                entity.BookReference = book.BookReference;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // D - DELETE One By GameId
        public bool DeleteBook(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Books
                    .Where(e => e.UserId == _userId)
                    .Single(e => e.BookId == id);
                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
