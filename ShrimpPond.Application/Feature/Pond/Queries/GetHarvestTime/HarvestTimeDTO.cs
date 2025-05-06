using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime
{
    public class HarvestTimeDTO
    {
        public string PondId { get; set; } = string.Empty;
        public int HarvestTime { get; set; }
        public List<string>? Amount { get; set; }   
    }
}
