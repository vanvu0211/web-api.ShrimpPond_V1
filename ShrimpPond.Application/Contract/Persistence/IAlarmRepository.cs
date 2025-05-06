using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Domain.Alarm;

namespace ShrimpPond.Application.Contract.Persistence
{
    public interface IAlarmRepository : IRepositoryBaseAsync<Alarm, int>
    {
    }
}
