using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Result
    {
        // Key
        [Key]
        public int ResultId { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        [ForeignKey(nameof(Bet))]
        public int BetId { get; set; }
        public Bet Bet { get; set; }
        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        // Virtual Lists

        // Variables
        public bool DidWin { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
