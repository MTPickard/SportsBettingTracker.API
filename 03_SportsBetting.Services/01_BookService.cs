using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class BookService
    {
        private readonly Guid _userId;

        public BookService(Guid userId)
        {
            _userId = userId;
        }

        // C POST CreateBook
        public bool CreateBook(BookCreate book)
        {
            var entity =
                new Book()
                {
                    MemberId = book.MemberId,
                    OwnerId = _userId,
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
                    .Where(e => e.OwnerId == _userId)
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
                    .Where(e => e.OwnerId == _userId)
                    .Single(e => e.BookId == id);
                return
                    new BookDetail
                    {
                        BookId = entity.BookId,
                        MemberId = entity.MemberId,
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
                    .Single(e => e.BookId == book.BookId && e.OwnerId == _userId);

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
                    .Single(e => e.BookId == bookId && e.OwnerId == _userId);

                ctx.Books.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
