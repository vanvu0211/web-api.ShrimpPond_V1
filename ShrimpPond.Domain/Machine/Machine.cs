using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Machine
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public List<PondId> PondIds { get; set; }
        public bool Status { get; set; }
        public int FarmId { get; set; }
        public Domain.Farm.Farm? Farm { get; set; }
    }
}
