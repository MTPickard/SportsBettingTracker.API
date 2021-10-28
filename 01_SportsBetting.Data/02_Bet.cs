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
        // Key
        [Key]
        public int BetId { get; set; }

        public Guid OwnerId { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }

        // Virtual Lists

        // Variables
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
