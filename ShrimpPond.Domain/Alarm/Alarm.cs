using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Alarm
{
    public class Alarm
    {
        public int AlarmId { get; set; }
        public string AlarmName { get; set; } = string.Empty;
        public string AlarmDetail { get; set; } = string.Empty;
        public DateTime AlarmDate { get; set; }

        public int FarmId { get; set; }
        public Domain.Farm.Farm Farm { get; set; }
    }
}
