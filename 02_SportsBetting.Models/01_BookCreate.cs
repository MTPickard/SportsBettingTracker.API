using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SportsBetting.Models
{
    public class BookCreate
    {
        public string Name { get; set; }

        public double Balance { get; set; }

        public string BookReference { get; set; }

        public int MemberId { get; set; }
    }
}
