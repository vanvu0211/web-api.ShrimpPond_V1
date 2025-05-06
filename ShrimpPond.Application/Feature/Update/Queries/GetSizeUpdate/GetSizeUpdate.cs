using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate
{
    public record GetSizeUpdate: IRequest<List<GetSizeUpdateDTO>>
    {
    }
}
