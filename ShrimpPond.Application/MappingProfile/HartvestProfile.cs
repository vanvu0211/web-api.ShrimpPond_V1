using AutoMapper;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Application.Feature.TimeSetting.Queries.GetTimeSetting;
using ShrimpPond.Domain.PondData.Harvest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class HartvestProfile: Profile
    {

        public HartvestProfile()
        {
            CreateMap<Harvest, HarvestDTO>();
        }
    }
}
