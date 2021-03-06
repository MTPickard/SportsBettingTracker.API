using _01_SportsBetting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SportsBetting.Models
{
    public class BookDetail
    {
        public int BookId { get; set; }

        public int MemberId { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string BookReference { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
