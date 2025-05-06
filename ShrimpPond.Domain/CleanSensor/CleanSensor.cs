using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.CleanSensor
{
    public class CleanSensor
    {
        public int CleanSensorId {  get; set; }
        public DateTime CleanTime { get; set; }

        public int FarmId { get; set; }
        public Domain.Farm.Farm Farm { get; set; }
    }
}
