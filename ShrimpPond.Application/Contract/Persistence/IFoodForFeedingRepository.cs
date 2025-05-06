using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface IFoodForFeedingRepository: IRepositoryBaseAsync<FoodForFeeding,int>
    {
    }
}
