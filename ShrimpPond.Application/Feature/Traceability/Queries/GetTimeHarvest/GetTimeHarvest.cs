using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTimeHarvest
{
    public class GetTimeHarvest: IRequest<List<TimeHarvest>>
    {
        public int farmId { get; set; }
    }
}
