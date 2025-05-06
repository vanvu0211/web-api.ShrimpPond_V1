using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding
{
    public class GetFoodFeedingDTO
    {
        public string PondId { get; set; } = string.Empty;
        public List<Foods>? Foods { get; set; }
        public DateTime FeedingDate { get; set; }
    }
}
