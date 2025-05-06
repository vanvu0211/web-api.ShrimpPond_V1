using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Environment.Queries.GetEnvironment
{
    public record GetEnvironment: IRequest< List<GetEnvironmentDTO>>
    {
        
    }
}
