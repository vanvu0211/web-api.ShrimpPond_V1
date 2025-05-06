using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Food
{
    public class FoodFeeding
    {
        public int FoodFeedingId { get; set; }
        public ICollection<FoodForFeeding>? Foods { get; set; }
        public DateTime FeedingDate { get; set; }
        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }
    }
}
