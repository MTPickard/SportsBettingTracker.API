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
        [ForeignKey(nameof(MemberId))]
        public int MemberId { get; set; }
        [ForeignKey(nameof(BetId))]
        public int BetId { get; set; }
        [ForeignKey(nameof(TransactionId))]

        // Virtual Lists
        public int TransactionId { get; set; }

        // Variables
        public bool DidWin { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
