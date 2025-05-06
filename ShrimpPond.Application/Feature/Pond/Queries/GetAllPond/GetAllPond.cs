using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetAllPond
{
    public record GetAllPond : IRequest<List<PondDTO>>
    {
        public int farmId { get; set; }
    }
}
