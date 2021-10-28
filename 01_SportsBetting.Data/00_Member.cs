using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Member
    {
        // Key
        [Key]
        public int MemberId { get; set; }

        // Virtual Lists
        public virtual List<Bet> _bets { get; set; }
        public virtual List<Book> _books { get; set; }
        public virtual List<Transaction> _transactions { get; set; }
        public virtual List<Result> _results { get; set; }

        // Variables
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
