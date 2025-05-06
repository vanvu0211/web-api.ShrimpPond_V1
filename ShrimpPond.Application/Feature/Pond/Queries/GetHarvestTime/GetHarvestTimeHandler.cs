using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime
{
    public class GetHarvestTimeHandler: IRequestHandler<GetHarvestTime,HarvestTimeDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<HarvestTimeDTO> _logger;

        public GetHarvestTimeHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<HarvestTimeDTO> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<HarvestTimeDTO> Handle(GetHarvestTime request, CancellationToken cancellationToken)
        {
            //query

            
            var data = new HarvestTimeDTO();

            var harvestTime = _unitOfWork.harvestRepository.FindByCondition(x => x.PondId == request.pondId).OrderBy(x => x).LastOrDefault();
            if (harvestTime == null)
            {
                data.HarvestTime = 0;
                data.PondId = request.pondId;

                return data;
            }
            List<string> Amounts = new();
            var harvests    = _unitOfWork.harvestRepository.FindByCondition(x=>x.PondId == request.pondId).ToList();

            foreach (var harvest in harvests)
            {
                Amounts.Add("Lần"+ harvest.HarvestTime.ToString() +":"+harvest.Amount.ToString());
            }
            data.Amount = Amounts;
            data.HarvestTime = harvestTime.HarvestTime;
            data.PondId = request.pondId;   

            return data;
        }
    }
}
