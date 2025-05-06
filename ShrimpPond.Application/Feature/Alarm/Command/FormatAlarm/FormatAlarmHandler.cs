using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Alarm.Queries.GetAllAlarm;
namespace ShrimpPond.Application.Feature.Alarm.Command.FormatAlarm
{
    public class FormatAlarmHandler: IRequestHandler<FormatAlarm,string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FormatAlarmHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(FormatAlarm request, CancellationToken cancellationToken)
        {
            //query
            var alarms = _unitOfWork.alarmRepository.FindAll();
            _unitOfWork.alarmRepository.RemoveRange(alarms);
            await _unitOfWork.SaveChangeAsync();
            //return
            return "OK";
        }
    }
}
