using MediatR;
using ShrimpPond.Domain.PondData.Harves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.HarvestPond
{
    public class HarvestPond : IRequest<HarvestPond>
    {
        public EHarvest harvestType { get; set; }
        public DateTime harvestDate { get; set; }
        public float amount { get; set; }
        //public string Unit { get; set; } = string.Empty;
        public float size { get; set; }
        public List<string>? certificates { get; set; }

        public string pondId { get; set; } = string.Empty;
        public int farmId { get; set; }
    }
}
