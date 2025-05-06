using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Queries.GetALlMachine
{
    public class GetALlMachine: IRequest<List<GetALlMachineDTO>>
    {
        public int farmId { get; set; }
    }
}
