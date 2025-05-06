using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine
{
    public class GetAllMedicineDTO
    {
        public int MedicineId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
