using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetTimeClean
{
    public class GetTimeCleanHandler : IRequestHandler<GetTimeClean, GetTimeCleanDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<HarvestTimeDTO> _logger;

        public GetTimeCleanHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<HarvestTimeDTO> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<GetTimeCleanDTO> Handle(GetTimeClean request, CancellationToken cancellationToken)
        {
            //query



            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var data = new GetTimeCleanDTO();
            var cleanTime = DateTime.UtcNow.AddHours(7);

            var firstTime = _unitOfWork.cleanSensorRepository.FindByCondition(x=>x.FarmId == farm.FarmId).OrderBy(x=>x.CleanSensorId).LastOrDefault();
            if (firstTime == null)
            {
                var firstPond = _unitOfWork.pondRepository.FindByCondition(x => x.FarmId == farm.FarmId).OrderBy(x => x.StartDate).FirstOrDefault();
                if (firstPond == null)
                {
                    cleanTime = DateTime.UtcNow.AddHours(7).AddDays(-1);

                }
                else cleanTime = firstPond.StartDate;             
            }
            else
            {
                cleanTime = firstTime.CleanTime;
            }

            data = new GetTimeCleanDTO()
            {
                CleanTime = cleanTime,
            };

            return data;
        }
    }
}
