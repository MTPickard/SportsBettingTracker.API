using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string TransactionNote { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
