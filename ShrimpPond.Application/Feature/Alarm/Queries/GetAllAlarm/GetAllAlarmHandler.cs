using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine;
namespace ShrimpPond.Application.Feature.Alarm.Queries.GetAllAlarm
{
    public class GetAllAlarmHandler: IRequestHandler<GetAllAlarm,List<GetAllAlarmDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetAllAlarm> _logger;

        public GetAllAlarmHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetAllAlarm> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<List<GetAllAlarmDTO>> Handle(GetAllAlarm request, CancellationToken cancellationToken)
        {
            //query

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var alarms = _unitOfWork.alarmRepository.FindByCondition(x => x.FarmId == farm.FarmId).ToList();

            //logging
            _logger.LogInformation("Get medicine successfully");
            // convert
            var data = _mapper.Map<List<GetAllAlarmDTO>>(alarms);
            //return
            return data;
        }
    }
}
