using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using ShrimpPond.Application.Contract.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShrimpPond.Domain.Medicine;

namespace ShrimpPond.Persistence.Repository
{
    public class MedicineRepository : RepositoryBase<Medicine, int>, IMedicineRepository
    {
        public MedicineRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
}
