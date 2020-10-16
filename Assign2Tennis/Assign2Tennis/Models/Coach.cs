using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign2Tennis.Models
{
    public class Coach
    {
        [Column("ID")]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        
    }
}
