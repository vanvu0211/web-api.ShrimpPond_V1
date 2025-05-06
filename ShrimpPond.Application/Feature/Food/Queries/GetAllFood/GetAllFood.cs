using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Queries.GetAllFood
{
    public class GetAllFood : IRequest<List<GetAllFoodDTO>>
    {
        public int farmId { get; set; } 

        public GetAllFood(int farmId)
        {
            this.farmId = farmId;
        }

       
    }
}
