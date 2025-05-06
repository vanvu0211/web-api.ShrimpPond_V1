using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Queries.GetAllPond
{
    public class GetAllPondHandler: IRequestHandler<GetAllPond,List<PondDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetAllPond> _logger;

        public GetAllPondHandler(IMapper mapper,IUnitOfWork unitOfWork,  IAppLogger<GetAllPond> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<PondDTO>> Handle(GetAllPond request, CancellationToken cancellationToken)
            {
            //query
            var data = new List<PondDTO>();
            var pondTypes =  _unitOfWork.pondTypeRepository.FindByCondition(x=>x.FarmId == request.farmId).ToList();
            if (pondTypes == null)
            {
                throw new BadRequestException("Not Found pondTypes");
            }

            foreach(var pondType in pondTypes)
            {

                var ponds = _unitOfWork.pondRepository.FindByCondition(x => x.PondTypeId == pondType.PondTypeId).ToList();

            foreach (var pond in ponds)
                {
                   
                    var dt = new PondDTO()
                    {
                        pondId = pond.PondId,
                        pondName = pond.PondName,
                        pondTypeName = pondType.PondTypeName,
                        pondTypeId = pondType.PondTypeId,
                        originPondId = pond.OriginPondId,
                        seedId = pond.SeedId,
                        amountShrimp = pond.AmountShrimp,
                        deep = pond.Deep,
                        diameter = pond.Diameter,
                        status = pond.Status
                    };

                    if (pond.OriginPondId == "")
                    {

                        dt.startDate = pond.StartDate;
                    }
                    else
                    {
                        var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.OriginPondId);
                        if (originPond == null)
                        {
                            throw new BadRequestException("Not Found Pond");
                        }
                        dt.startDate = originPond.StartDate;
                    }



                    data.Add(dt);
                }
            }

            
            //logging
            _logger.LogInformation("Get pond successfully");
            // convert
            //var data = _mapper.Map<List<PondDTO>>(pondTypes);
            //return
            return data;
        }
    }
}
