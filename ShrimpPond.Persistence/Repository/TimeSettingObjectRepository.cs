using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
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
    public class TimeSettingObjectRepository : RepositoryBase<TimeSettingObject, int>, ITimeSettingObjectRepository
    {
        public TimeSettingObjectRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext) { }
    }
}
