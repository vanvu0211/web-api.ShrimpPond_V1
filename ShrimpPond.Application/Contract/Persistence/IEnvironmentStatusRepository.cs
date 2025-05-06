using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface IEnvironmentStatusRepository: IRepositoryBaseAsync<Domain.Environments.EnvironmentStatus, int>
    {
    }
}
