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

        public double oxiTop { get; set; }
        public double oxiLow { get; set; }

        public double temperatureTop { get; set; }
        public double temperatureLow { get; set; }

        public int FarmId { get; set; }
        public Domain.Farm.Farm? Farm { get; set; }
    }
}
