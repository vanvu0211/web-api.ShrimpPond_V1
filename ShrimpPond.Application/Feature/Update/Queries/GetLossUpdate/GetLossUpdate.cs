using MediatR;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate
{
    public record GetLossUpdate : IRequest<List<GetLossUpdateDTO>>
    {
    }
}
