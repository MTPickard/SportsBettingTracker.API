using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Transaction
    {
        // Key
        [Key]
        public int TransactionId { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(MemberId))]
        public int MemberId { get; set; }
        [ForeignKey(nameof(BookId))]
        public int BookId { get; set; }

        // Variables
        // deposit or winnings
        public double Credit { get; set; }
        // withdrawal or loss
        public double Debit { get; set; }
        public string TransactionNote { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
