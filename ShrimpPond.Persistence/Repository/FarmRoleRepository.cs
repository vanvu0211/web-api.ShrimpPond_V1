using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    public class FarmRoleRepository : RepositoryBase<FarmRole, Guid>,IFarmRoleRepository
    {
        public FarmRoleRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
  
}
