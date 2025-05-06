using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Domain.Farm;

namespace ShrimpPond.Domain.PondData
{
    public class PondType
    {
        public string PondTypeId {  get; set; } = string.Empty;
        public string PondTypeName { get; set; } = string.Empty;

        public int FarmId { get; set; }
        public Domain.Farm.Farm Farm { get; set; }
    }
}
