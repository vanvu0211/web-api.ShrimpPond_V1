using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Farm
{
    public class Farm
    {
        public int FarmId { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string FarmName { get; set;} = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
