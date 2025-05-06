using AutoMapper;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Domain.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class FoodProfile: Profile
    {
        public FoodProfile()
        {
            CreateMap<Food,GetAllFoodDTO>();
        }
    }
}
