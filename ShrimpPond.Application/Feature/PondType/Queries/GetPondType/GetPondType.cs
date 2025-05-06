using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Queries.GetPondType
{
    public record GetPondType : IRequest<List<PondTypeDto>>;
}
