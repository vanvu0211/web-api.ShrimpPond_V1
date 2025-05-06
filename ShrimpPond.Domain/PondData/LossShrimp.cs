using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class LossShrimp
    {
        public int LossShrimpId {  get; set; }
        public float LossValue { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }

    }
}
