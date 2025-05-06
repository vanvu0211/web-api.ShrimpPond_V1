using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding
{
    public class GetMedicineFeedingDTO
    {
        public string pondId { get; set; } = string.Empty;
        public List<Medicines>? medicines { get; set; }
        public DateTime feedingDate { get; set; }
    }
}
