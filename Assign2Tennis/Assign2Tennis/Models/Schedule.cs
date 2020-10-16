using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign2Tennis.Models
{
    public class Schedule
    {
        [Column("ID")]
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime Time { get; set; }      
        public string Coach { get; set; }
        public string Location { get; set; }
    }
}
