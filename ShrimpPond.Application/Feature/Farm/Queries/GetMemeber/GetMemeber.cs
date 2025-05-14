using MediatR;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetMemeber
{
    public class GetMemeber : IRequest<List<GetMemeberDTO>>
    {
        public int FarmId { get; set; }
    }
}
