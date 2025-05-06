using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Medicine
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int FarmId { get; set; }
        public Domain.Farm.Farm? Farm { get; set; }
    }
}
