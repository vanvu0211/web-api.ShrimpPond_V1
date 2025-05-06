using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability
{
    public class TraceabilityDTO
    {
        public string SeedId { get; set; } = string.Empty;
        public string HarvestPondId { get; set; } = string.Empty;
        public float TotalAmount {  get; set; }
        public int HarvestTime { get; set; }
        public string Size { get; set; } = string.Empty;
        public List<byte[]> Certificates { get; set; } = new List<byte[]>();
        public int DaysOfRearing { get; set; }
        public string FarmName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
