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
    public class TransactionService
    {
        private readonly int _userId;

        public TransactionService (int userId)
        {
            _userId = userId;
        }

        // C POST CreateNewTransaction
        public bool CreateNewTransaction(TransactionCreate model)
        {
            var entity =
                new Transaction()
                {
                    MemberId = _userId,
                    TransactionId = model.TransactionId,
                    Credit = model.Credit,
                    Debit = model.Debit,
                    TransactionNote = model.TransactionNote,
                    CreatedUtc = model.CreatedUtc
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
                        .Where(e => e.MemberId == _userId)
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
                        .Single(e => e.TransactionId == id && e.MemberId == _userId);
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
                        .Single(e => e.TransactionId == model.TransactionId && e.MemberId == _userId);

                entity.Credit = model.Credit;
                entity.Debit = model.Debit;
                entity.TransactionNote = model.TransactionNote;
                entity.ModifiedUtc = model.ModifiedUtc;

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
                      .Single(e => e.TransactionId == transactionId && e.MemberId == _userId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
