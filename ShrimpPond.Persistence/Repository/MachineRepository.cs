using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Machine;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    class MachineRepository : RepositoryBase<Machine, int>, IMachineRepository
    {
        public MachineRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }

}
