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
    public class ResultService
    {
        private readonly int _userId;

        public ResultService(int userId)
        {
            _userId = userId;
        }

        public bool CreateResult(ResultCreate model)
        {
            var entity =
                new Result()
                {
                    ResultId = _userId,
                    DidWin = model.DidWin,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Results.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ResultListItem> GetResults()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Results
                        .Where(e => e.ResultId == _userId)
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

        public ResultDetail GetResultById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Results
                        .Single(e => e.ResultId == id && e.ResultId == _userId);
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

        public bool UpdateResult(ResultEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Results
                        .Single(e => e.ResultId == model.ResultId && e.UserId == _userId);

                entity.DidWin = model.DidWin;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteResult(int resultId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Results
                        .Single(e => e.ResultId == resultId && e.UserId == _userId);

                ctx.Results.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
