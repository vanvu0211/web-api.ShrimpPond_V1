
using System.Runtime;
namespace ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;

public class TimeSettingObject
{
    public int TimeSettingObjectId { get; set; }
    public int Index { get; set; }
    public string Time { get; set; } = string.Empty;

    public int TimeSettingId { get; set; }
    public Domain.TimeSetting.TimeSetting? TimeSetting { get; set; }

}

