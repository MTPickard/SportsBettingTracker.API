using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SportsBetting.Models
{
    public class ResultListItem
    {
        public int ResultId { get; set; }

        public int MemberId { get; set; }
        public int BetId { get; set; }
        public int TransactionId { get; set; }

        public bool DidWin { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
