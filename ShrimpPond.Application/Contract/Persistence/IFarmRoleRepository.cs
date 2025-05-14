using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface IFarmRoleRepository : IRepositoryBaseAsync<FarmRole, Guid>
    {
    }
}
