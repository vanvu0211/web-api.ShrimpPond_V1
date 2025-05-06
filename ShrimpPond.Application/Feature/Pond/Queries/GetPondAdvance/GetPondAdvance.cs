using MediatR;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetPondAdvance
{
    public class GetPondAdvance : IRequest<List<GetPondAdvanceDTO>>
    {
        public string userName { get; set; } = string.Empty;
        public string farmName { get; set; } = string.Empty;
        
    }
}
