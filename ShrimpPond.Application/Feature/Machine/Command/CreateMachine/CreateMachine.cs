using MediatR;
using ShrimpPond.Domain.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Command.CreateMachine
{
    public class CreateMachine: IRequest<string>
    {
        public int farmId { get; set; } 
        public string machineName { get; set; } = string.Empty;
        public List<PondId> pondIds { get; set; }
    }
}
