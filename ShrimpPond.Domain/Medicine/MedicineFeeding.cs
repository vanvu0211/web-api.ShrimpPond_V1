using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Medicine
{
    public class MedicineFeeding
    {
        public int MedicineFeedingId { get; set; }
        public ICollection<MedicineForFeeding>? Medicines { get; set; }
        public DateTime FeedingDate { get; set; }
        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }

    }
}
