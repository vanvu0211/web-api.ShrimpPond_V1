using ShrimpPond.Application.Contract.Persistence;
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
    public class CertificateRepository :RepositoryBase<Certificate, int>, ICertificateRepository
    {
        public CertificateRepository(ShrimpPondDbContext shrimpPondDbContext) : base(shrimpPondDbContext)
        {

        }
    }
}
