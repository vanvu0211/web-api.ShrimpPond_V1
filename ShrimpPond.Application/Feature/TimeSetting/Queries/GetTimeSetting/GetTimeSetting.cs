using MediatR;

namespace ShrimpPond.Application.Feature.TimeSetting.Queries.GetTimeSetting;

public  class GetTimeSetting: IRequest<List<GetTimeSettingDto>>
{
    public int farmId { get; set; } 
}