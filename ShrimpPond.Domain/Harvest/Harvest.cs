using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.PondData.Harves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData.Harvest
{
    public class Harvest
    {
        public int HarvestId { get; set; }
        public int HarvestTime { get; set; }
        public EHarvest HarvestType { get; set; }
        public string SeedId { get; set; } = string.Empty;

        public List<Certificate>? Certificates { get; set; }
        public float Size { get; set; }
        public float Amount { get; set; }
        public DateTime HarvestDate { get; set; }
        public string PondId { get; set; } = string.Empty;
        public Domain.PondData.Pond? Pond { get; set; }
        public int  FarmId { get; set; } 
        public Domain.Farm.Farm? Farm { get; set; }

    }
}
