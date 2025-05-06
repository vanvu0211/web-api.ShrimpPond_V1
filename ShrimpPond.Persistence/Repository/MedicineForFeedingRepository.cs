using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Medicine;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    public class MedicineForFeedingRepository : RepositoryBase<MedicineForFeeding, int>, IMedicineForFeedingRepository
    {
        public MedicineForFeedingRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
}
