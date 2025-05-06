using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability
{
    public class GetTraceability: IRequest< TraceabilityDTO>
    {
        public string seedId {  get; set; } = string.Empty;
        public int harvestTime { get; set; }
        public int farmId { get; set; }
    }
}
