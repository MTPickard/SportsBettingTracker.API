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
    public class BetService
    {
        private readonly int _userId;

        public BetService(int userId)
        {
            _userId = userId;
        }

        public bool CreateBet(BetCreate bet)
        {
            var entity =
                new Bet()
                {
                    UserId = _userId,
                    BetId = bet.BetId,
                    MatchUp = bet.MatchUp,
                    BetParameters = bet.BetParameters,
                    BetAmount = bet.BetAmount,
                    ToWin = bet.ToWin,
                    IsResolved = bet.IsResolved,
                    CreatedUTC = DateTimeOffset.Now
                };

            using(var ctx = new ApplicationDbContext())
            {
                ctx.Bets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BetListItem> GetBets()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Bets
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                            new BetListItem
                            {
                                BetId = e.BetId,
                                MatchUp = e.MatchUp,
                                BetParameters = e.BetParameters,
                                ToWin = e.ToWin,
                                IsResolved = e.IsResolved,
                                CreatedUTC = e.CreatedUTC
                            }
                        );
                return query.ToArray();
            }
        }

        public BetDetail GetBetById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bets
                    .Single(e => e.BetId == id && e.UserId == _userId);
                return
                    new BetDetail
                    {
                        BetId = entity.BetId,
                        MatchUp = entity.MatchUp,
                        BetAmount = entity.BetAmount,
                        BetParameters = entity.BetParameters,
                        ToWin = entity.ToWin,
                        IsResolved = entity.IsResolved,
                        CreatedUTC = entity.CreatedUTC
                    };
            }
        }
        
        public bool UpdateBet(BetEdit bet)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bets
                    .Single(e => e.BetId == bet.BetId && e.UserId == _userId);

                entity.MatchUp = bet.MatchUp;
                entity.BetAmount = bet.BetAmount;
                entity.BetParameters = bet.BetParameters;
                entity.ToWin = bet.ToWin;
                entity.IsResolved = bet.IsResolved;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBet(int betId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bets
                    .Single(e => e.BetId == betId && e.UserId == _userId);

                ctx.Bets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
