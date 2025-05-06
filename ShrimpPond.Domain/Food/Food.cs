using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Food
{
    public class Food
    {
        public int FoodId { get; set; }
        public string Name { get; set; } = string.Empty;

        public int FarmId { get; set; }
        public Farm.Farm Farm { get; set; }
    }
}
