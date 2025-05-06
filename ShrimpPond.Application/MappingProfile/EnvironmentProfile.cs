using AutoMapper;
using ShrimpPond.Application.Feature.Environment.Queries.GetEnvironment;
using ShrimpPond.Domain.Environments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class EnvironmentProfile: Profile
    {
        public EnvironmentProfile() 
        {
            CreateMap<EnvironmentStatus, GetEnvironmentDTO>();
        }
    }
}
