using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class SizeShrimp
    {
        public int SizeShrimpId { get; set; }
        public float SizeValue { get; set; }

        public DateTime UpdateDate { get; set; }
        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }
    }
}
