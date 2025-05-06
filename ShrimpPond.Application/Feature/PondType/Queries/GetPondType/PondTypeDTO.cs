using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Queries.GetPondType
{
    public class PondTypeDto
    {
        public string pondTypeId { get; set; } = string.Empty;
        public string pondTypeName { get; set; } = string.Empty;
        public int farmId {  get; set; } 
    }
}
