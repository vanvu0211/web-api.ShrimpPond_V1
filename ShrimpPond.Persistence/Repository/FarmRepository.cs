using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.PondData.Harvest;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
 
    public class FarmRepository : RepositoryBase<Farm, int>, IFarmRepository
    {
        public FarmRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {
        }
    }
}
