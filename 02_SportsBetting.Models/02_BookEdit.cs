using _01_SportsBetting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SportsBetting.Models
{
    public class BookEdit
    {
        public int BookId { get; set; }

        public virtual List<Transaction> _transactions { get; set; }

        public virtual List<Bet> _bets { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string BookReference { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
