using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm
{
    public class FarmDTO
    {
        public int farmId { get; set; } 
        public string farmName { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
