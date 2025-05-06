using MediatR;
using ShrimpPond.Application.Feature.Machine.Command.CreateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Command.UpdateMachine
{
    public class UpdateMachine: IRequest<string>
    {
        public int machineId { get; set; }

        public List<PondId> pondIds { get; set; }
    }
}
