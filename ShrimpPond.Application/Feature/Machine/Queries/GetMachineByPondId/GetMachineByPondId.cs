using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Queries.GetMachineByPondId
{
    public class GetMachineByPondId: IRequest<GetMachineByPondIdDTO>
    {
        public string pondId { get; set; } = string.Empty;
    }
}
