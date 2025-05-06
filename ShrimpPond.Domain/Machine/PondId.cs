using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Machine
{
    public class PondId
    {
        public int Id { get; set; }
        public string PondIdForMachine { get; set; } = string.Empty;
        public string PondName { get; set; } = string.Empty;
        public int MachineId { get; set; }
    }
}
