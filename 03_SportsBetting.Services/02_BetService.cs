using _01_SportsBetting.Data;
using _02_SportsBetting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_SportsBetting.Services
{
    public class BetService
    {
        private readonly Guid _userId;

        public BetService(Guid userId)
        {
            _userId = userId;
        }

        // C POST CreateBet
        public bool CreateBet(BetCreate bet)
        {
            var entity =
                new Bet()
                {
                    OwnerId = _userId,
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

        // R GET ViewBetsByUserId
        public IEnumerable<BetListItem> ViewBetsByUserId()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Bets
                    .Where(e => e.OwnerId == _userId)
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

        // R GET ViewBetByBetId
        public BetDetail ViewBetByBetId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bets
                    .Single(e => e.BetId == id && e.OwnerId == _userId);
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

        // U PUT UpdateBetByBetId
        public bool UpdateBetByBetId(BetEdit bet)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bets
                    .Single(e => e.BetId == bet.BetId && e.OwnerId == _userId);

                entity.MatchUp = bet.MatchUp;
                entity.BetAmount = bet.BetAmount;
                entity.BetParameters = bet.BetParameters;
                entity.ToWin = bet.ToWin;
                entity.IsResolved = bet.IsResolved;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        // D DELETE RemoveBetByBetId
        public bool RemoveBetByBetId(int betId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Bets
                    .Single(e => e.BetId == betId && e.OwnerId == _userId);

                ctx.Bets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
