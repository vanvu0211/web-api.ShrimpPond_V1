using AutoMapper;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Application.Feature.TimeSetting.Queries.GetTimeSetting;

namespace ShrimpPond.Application.MappingProfile;

public class TimeSettingProfile: Profile
{
    public TimeSettingProfile()
    {
        CreateMap<TimeSettingObject, GetTimeSettingDto>();
    }
}