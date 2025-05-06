using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Command.DeleteMachine
{
    public class DeleteMachine: IRequest<int>
    {
        public int machineId { get; set; }
    }
}
