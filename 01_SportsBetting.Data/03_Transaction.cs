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

        public Guid OwnerId { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; }

        // Variables
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string TransactionNote { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
