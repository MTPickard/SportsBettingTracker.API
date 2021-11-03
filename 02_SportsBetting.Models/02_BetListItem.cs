using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SportsBetting.Models
{
    public class BetListItem
    {
        public int MemberId { get; set; }

        public int BetId { get; set; }

        public string MatchUp { get; set; }

        public string BetDescription { get; set; }

        public decimal BetAmount { get; set; }

        public double BetOdds { get; set; }

        public decimal ToWin { get; set; }

        public bool IsResolved { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }
    }
}
