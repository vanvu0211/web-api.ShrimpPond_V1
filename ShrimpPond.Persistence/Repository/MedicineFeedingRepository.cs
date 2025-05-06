using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Contract.Persistence.Genenric;
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
    public class MedicineFeedingRepository: RepositoryBase<MedicineFeeding,int>, IMedicineFeedingRepository
    {
        public MedicineFeedingRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
}
