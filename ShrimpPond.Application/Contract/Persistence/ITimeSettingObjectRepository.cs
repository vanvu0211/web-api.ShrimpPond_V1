using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Domain.TimeSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface ITimeSettingObjectRepository : IRepositoryBaseAsync<TimeSettingObject, int>
    {
    }
}
