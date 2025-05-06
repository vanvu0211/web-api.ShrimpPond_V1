using ShrimpPond.Domain.Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Alarm.Queries.GetAllAlarm
{
    public class GetAllAlarmDTO
    {
        public int AlarmId { get; set; }
        public string AlarmName { get; set; } = string.Empty;
        public string AlarmDetail { get; set; } = string.Empty;
        public DateTime AlarmDate { get; set; }
    }
}
