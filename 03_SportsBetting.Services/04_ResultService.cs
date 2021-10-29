using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class ResultService
    {
        private readonly Guid _userId;

        public ResultService(Guid userId)
        {
            _userId = userId;
        }

        // C POST CreateResult
        public bool CreateResult(ResultCreate model)
        {
            var entity =
                new Result()
                {
                    OwnerId = _userId,
                    MemberId = model.MemberId,
                    BetId = model.BetId,
                    TransactionId = model.TransactionId,
                    DidWin = model.DidWin,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Results.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // R GET ViewAllResultsByUserId
        public IEnumerable<ResultListItem> ViewResultsByUserId()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Results
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ResultListItem
                                {
                                    ResultId = e.ResultId,
                                    DidWin = e.DidWin,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }

        // R GET ViewOneResultByResultId
        public ResultDetail ViewResultByResultId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Results
                        .Single(e => e.ResultId == id && e.OwnerId == _userId);
                return
                    new ResultDetail
                    {
                        ResultId = entity.ResultId,
                        DidWin = entity.DidWin,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // U PUT UpdateResultByResultId
        public bool UpdateResultByResultId(ResultEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Results
                        .Single(e => e.ResultId == model.ResultId && e.OwnerId == _userId);

                entity.DidWin = model.DidWin;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // D DELETE RemoveResultByResultId
        public bool RemoveResultByResultId(int resultId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Results
                        .Single(e => e.ResultId == resultId && e.OwnerId == _userId);

                ctx.Results.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
