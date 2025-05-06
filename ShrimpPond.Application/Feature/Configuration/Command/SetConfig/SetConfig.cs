using MediatR;
using ShrimpPond.Domain.Configuration;

namespace ShrimpPond.Application.Feature.Configuration.Command.SetConfig
{
    public class SetConfig : IRequest<Guid>
    {
        public double pHTop { get; set; }
        public double pHLow { get; set; }

        public double oxiTop { get; set; }
        public double oxiLow { get; set; }

        public double temperatureTop { get; set; }
        public double temperatureLow { get; set; }
        public int FarmId { get; set; }
    }
}
