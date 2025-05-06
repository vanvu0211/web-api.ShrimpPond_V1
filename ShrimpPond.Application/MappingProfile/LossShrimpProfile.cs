using AutoMapper;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class LossShrimpProfile : Profile
    {
        public LossShrimpProfile()
        {
            CreateMap<LossShrimp, GetLossUpdateDTO>();
        }
    }
}
