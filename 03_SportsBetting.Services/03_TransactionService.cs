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

        //Post CreateNewTransaction
        public bool CreateNewTransaction(TransactionCreate model)
        {
            var entity =
                new Transaction()
                {
                    UserId = _userId,
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

        // Get All Transactions by UserId
        public IEnumerable<TransactionListItem> GetAllTransactionsByUserId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transactions
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new TransactionListItem
                                {
                                    UserId = e.UserId,
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

        // Get One Transaction by TransactionId
        public TransactionDetail GetOneTransactionByTransactionId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == id && e.UserId == _userId);
                return
                    new TransactionDetail
                    {
                        UserId = entity.UserId,
                        TransactionId = entity.TransactionId,
                        Credit = entity.Credit,
                        Debit = entity.Debit,
                        TransactionNote = entity.TransactionNote,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        //Update Transaction By TransactionID
        public bool UpdateTransactionByTransactionId(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId && e.UserId == _userId);

                entity.Credit = model.Credit;
                entity.Debit = model.Debit;
                entity.TransactionNote = model.TransactionNote;
                entity.ModifiedUtc = model.ModifiedUtc;

                return ctx.SaveChanges() == 1;
            }
        }

        //Delete Transaction by TransactionID
        public bool RemoveTransactionByTransactionId(int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                      .Transactions
                      .Single(e => e.TransactionId == transactionId && e.UserId == _userId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
