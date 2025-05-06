using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

namespace ShrimpPond.Domain.TimeSetting
{
    public class TimeSetting
    {
        public int TimeSettingId { get; set; }
        public ICollection<TimeSettingObject>? TimeSettingObjects { get; set; }

        public bool EnableFarm { get; set; }
        public int FarmId { get; set; }
        public Domain.Farm.Farm Farm { get; set; }
    }
}
