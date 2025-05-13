using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Alarm.Queries.GetAllAlarm
{
    public class GetAllAlarm: IRequest<List<GetAllAlarmDTO>>
    {
        public int FarmId { get; set; }
    }
}
