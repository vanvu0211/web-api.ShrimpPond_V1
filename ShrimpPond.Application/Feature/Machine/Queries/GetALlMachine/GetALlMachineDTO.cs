using ShrimpPond.Application.Feature.Machine.Command.CreateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Queries.GetALlMachine
{
    public class GetALlMachineDTO
    {
        public int machineId { get; set; }
        public string machineName { get; set; } = string.Empty;
        public bool status { get; set; }
        public List<PondId> pondIds { get; set; }
    }
}
