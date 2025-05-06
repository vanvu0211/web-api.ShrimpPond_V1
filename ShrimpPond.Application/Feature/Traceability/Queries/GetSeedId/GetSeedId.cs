using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetSeedId
{
    public class GetSeedId: IRequest<List<SeedIdDTO>>
    {
        public int farmId { get; set; }
    }
}
