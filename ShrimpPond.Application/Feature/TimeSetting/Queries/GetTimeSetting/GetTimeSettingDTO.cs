using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

namespace ShrimpPond.Application.Feature.TimeSetting.Queries.GetTimeSetting;

public  class GetTimeSettingDto
{
    public int Index { get; set; }
    public string Time { get; set; } = string.Empty;
}