using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assign2Tennis.Models
{
    public class ScheduleMembers
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("ScheduleID")]
        public int ScheduleId { get; set; }
        public string Event { get; set; }
        public string Member { get; set; }
    }
}
