using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Bet
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        [Required]
        public int BetId { get; set; }

        [Required]
        public string MatchUp { get; set; }

        public string BetParameters { get; set; }

        [Required]
        public decimal BetAmount { get; set; }

        [Required]
        public decimal ToWin { get; set; }

        public bool IsResolved { get; set; }

        [Required]
        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}
