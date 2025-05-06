using AutoMapper;
using ShrimpPond.Application.Feature.Environment.Queries.GetEnvironment;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class CertificateProfile: Profile
    {
        public CertificateProfile()
        {
            CreateMap<Certificate, GetCertificates>();
        }
    }
}
