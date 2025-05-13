using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Configuration
{
    public class Configuration
    {
        public Guid Id { get; set; }
      
        public double pHTop { get; set; }
        public double pHLow { get; set; }

        public double OxiTop { get; set; }
        public double OxiLow { get; set; }

        public double TemperatureTop { get; set; }
        public double TemperatureLow { get; set; }

        public int FarmId { get; set; }
        public Domain.Farm.Farm? Farm { get; set; }
    }
}
