using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Queries.GetMachineByPondId
{
    public class GetMachineByPondIdDTO
    {
        public string pondId { get; set; } = string.Empty;
        public List<MachineData> machineDatas { get; set; }
    }
}
