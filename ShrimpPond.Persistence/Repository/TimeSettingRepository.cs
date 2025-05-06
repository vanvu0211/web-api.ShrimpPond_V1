using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.TimeSetting;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    public class TimeSettingRepository : RepositoryBase<TimeSetting, int>, ITimeSettingRepository
    {
        public TimeSettingRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext) { }
    }
}
