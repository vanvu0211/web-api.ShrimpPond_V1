using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Medicine
{
    public class MedicineForFeeding
    {

        public int MedicineForFeedingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public float Amount { get; set; }
        public int MedicineFeedingId { get; set; }
        public MedicineFeeding? MedicineFeeding { get; set; }
       
    }
}
