using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Queries.GetMachineByPondId
{
    public class MachineData
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public bool MachineStatus { get; set; } 
    }
}
