using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SportsBetting.Models
{
    public class TransactionCreate
    {
        public int MemberId { get; set; }
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string TransactionNote { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
