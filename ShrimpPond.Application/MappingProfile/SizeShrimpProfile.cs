using AutoMapper;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class SizeShrimpProfile:Profile
    {
        public SizeShrimpProfile() 
        {
            CreateMap<SizeShrimp,GetSizeUpdateDTO>();
        }
    }
}
