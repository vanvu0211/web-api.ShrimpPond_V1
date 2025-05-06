using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate
{
    public class GetLossUpdateDTO
    {
        public string PondId { get; set; } = string.Empty;
        public float LossValue { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
