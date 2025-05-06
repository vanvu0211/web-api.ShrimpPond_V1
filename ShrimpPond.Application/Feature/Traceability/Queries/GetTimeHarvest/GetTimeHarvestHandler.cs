using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTimeHarvest
{
    public class GetTimeHarvestHandler: IRequestHandler<GetTimeHarvest, List<TimeHarvest>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetTimeHarvest> _logger;

        public GetTimeHarvestHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetTimeHarvest> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<TimeHarvest>> Handle(GetTimeHarvest request, CancellationToken cancellationToken)
        {
            
            var result = new List<TimeHarvest>();

            var harverts = _unitOfWork.harvestRepository.FindByCondition(x => x.FarmId == request.farmId);
            foreach (var harvert in harverts)
            {
                if (result.Count(x => x.HarvestTime == harvert.HarvestTime)!=0) continue;

                result.Add(new TimeHarvest() { HarvestTime = harvert.HarvestTime });
            }

            return result;
        }
    }
}
