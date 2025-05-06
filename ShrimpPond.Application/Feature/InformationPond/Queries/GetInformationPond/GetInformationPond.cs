using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond
{
    public class GetInformationPond: IRequest<GetInformationPondDTO>
    {
        public string pondId { get; set; } = string.Empty;
    }
}
