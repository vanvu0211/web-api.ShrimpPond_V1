using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Queries.GetAllFood
{
    public class GetAllFoodDTO
    {
        public int foodId { get; set; }
        public string name { get; set; } = string.Empty;
        public int farmId { get; set; } 

    }
}
