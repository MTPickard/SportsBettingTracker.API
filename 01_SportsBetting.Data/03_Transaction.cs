using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Transaction
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        [ForeignKey(nameof(BookId))]
        public int BookId { get; set; }
        public int TransactionId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string TransactionNote { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
