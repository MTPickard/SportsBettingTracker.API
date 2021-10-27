using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }

        public virtual List<Transaction> _transactions { get; set; }

        public virtual List<Bet> _bets { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string BookReference { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
