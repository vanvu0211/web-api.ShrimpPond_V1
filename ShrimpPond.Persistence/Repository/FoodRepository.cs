using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Food;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    public class FoodRepository: RepositoryBase<Food,int >, IFoodRepository
    {
        public FoodRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {
        }
    }
}
