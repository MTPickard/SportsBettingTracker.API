using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService (Guid userId)
        {
            _userId = userId;
        }

        // C POST CreateNewTransaction
        public bool CreateNewTransaction(TransactionCreate model)
        {
            var entity =
                new Transaction()
                {
                    OwnerId = _userId,
                    MemberId = model.MemberId,
                    BookId = model.BookId,
                    TransactionId = model.TransactionId,
                    Credit = model.Credit,
                    Debit = model.Debit,
                    TransactionNote = model.TransactionNote,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // R GET ViewTransactionsByUserId 
        public IEnumerable<TransactionListItem> GetAllTransactionsByUserId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transactions
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new TransactionListItem
                                {
                                    MemberId = e.MemberId,
                                    TransactionId = e.TransactionId,
                                    Credit = e.Credit,
                                    Debit = e.Debit,
                                    TransactionNote = e.TransactionNote,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        // R GET ViewTransactionByTransactionId
        public TransactionDetail GetOneTransactionByTransactionId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == id && e.OwnerId == _userId);
                return
                    new TransactionDetail
                    {
                        MemberId = entity.MemberId,
                        TransactionId = entity.TransactionId,
                        Credit = entity.Credit,
                        Debit = entity.Debit,
                        TransactionNote = entity.TransactionNote,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        // U PUT UpdateTransactionByTransactionId
        public bool UpdateTransactionByTransactionId(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId && e.OwnerId == _userId);

                entity.Credit = model.Credit;
                entity.Debit = model.Debit;
                entity.TransactionNote = model.TransactionNote;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // D DELETE RemoveTransactionByTransactionId
        public bool RemoveTransactionByTransactionId(int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                      .Transactions
                      .Single(e => e.TransactionId == transactionId && e.OwnerId == _userId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
