using ShrimpPond.Domain.PondData.Harves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond
{
    public class HarvestDTO
    {
        public int harvestTime { get; set; }
        public EHarvest harvestType { get; set; }
        public string seedId { get; set; } = string.Empty;
        public float size { get; set; }
        public float amount { get; set; }
        public DateTime harvestDate { get; set; }
    }
}
