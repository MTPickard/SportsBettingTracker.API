using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_SportsBetting.Data
{
    public class Result
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
    }
}
