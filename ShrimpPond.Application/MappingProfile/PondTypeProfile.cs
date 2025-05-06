using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class PondTypeProfile :Profile
    {
        public PondTypeProfile() 
        {
            CreateMap<PondType, PondTypeDto>().ReverseMap();

        }
    }
}
