using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Domain.Configuration;
using ShrimpPond.Domain.PondData.CleanSensor;
using ShrimpPond.Persistence.DatabaseContext;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository
{
    public class ConfigurationRepository : RepositoryBase<Configuration, Guid>, IConfigurationRepository
    {
        public ConfigurationRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
}
