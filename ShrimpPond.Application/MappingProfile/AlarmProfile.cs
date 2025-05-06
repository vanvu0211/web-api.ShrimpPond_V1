using AutoMapper;
using ShrimpPond.Application.Feature.Alarm.Queries.GetAllAlarm;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;
using ShrimpPond.Domain.Alarm;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.MappingProfile
{
    public class AlarmProfile: Profile
    {
        public AlarmProfile()
        {
            CreateMap<Alarm, GetAllAlarmDTO>();
        }
    }
}
