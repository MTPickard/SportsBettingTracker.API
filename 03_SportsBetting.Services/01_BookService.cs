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
    public class BookService
    {
        private readonly int _userId;

        public BookService(int userId)
        {
            _userId = userId;
        }

        // C POST CreateBook
        public bool CreateBook(BookCreate book)
        {
            var entity =
                new Book()
                {
                    Name = book.Name,
                    Balance = book.Balance,
                    BookReference = book.BookReference,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Books.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // R GET ViewBooksByUserId
        public IEnumerable<BookListItem> ViewBooksByUserId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Books
                    .Where(e => e.MemberId == _userId)
                    .Select(
                        e =>
                            new BookListItem
                            {
                                BookId = e.BookId,
                                Name = e.Name,
                                Balance = e.Balance,
                                BookReference = e.BookReference,
                                CreatedUtc = e.CreatedUtc,
                                ModifiedUtc = e.ModifiedUtc
                            }
                        );
                return query.ToArray();
            }
        }

        // R GET ViewBookByBookId
        public BookDetail ViewBookByBookId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Books
                    .Where(e => e.MemberId == _userId)
                    .Single(e => e.BookId == id);
                return
                    new BookDetail
                    {
                        BookId = entity.BookId,
                        MemberId = entity.MemberId,
                        _transactions = entity._transactions,
                        _bets = entity._bets,
                        Name = entity.Name,
                        Balance = entity.Balance,
                        BookReference = entity.BookReference,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // U PUT UpdateBookByBookId
        public bool UpdateBookByBookId(BookEdit book)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Books
                    .Single(e => e.BookId == book.BookId && e.MemberId == _userId);

                entity._transactions = book._transactions;
                entity._bets = book._bets;
                entity.Name = book.Name;
                entity.Balance = book.Balance;
                entity.BookReference = book.BookReference;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // D DELETE RemoveBookByBookId
        public bool RemoveBookByBookId(int bookId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Books
                    .Single(e => e.BookId == bookId && e.MemberId == _userId);

                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
