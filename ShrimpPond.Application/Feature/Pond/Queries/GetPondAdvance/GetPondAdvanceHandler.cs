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

namespace ShrimpPond.Application.Feature.Pond.Queries.GetPondAdvance
{
    public class GetPondAdvanceHandler:  IRequestHandler<GetPondAdvance, List<GetPondAdvanceDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetPondAdvance> _logger;

        public GetPondAdvanceHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetPondAdvance> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<GetPondAdvanceDTO>> Handle(GetPondAdvance request, CancellationToken cancellationToken)
        {
            //query
            var data = new List<GetPondAdvanceDTO>();


            var farm = _unitOfWork.farmRepository.FindByCondition(x => x.FarmName == request.FarmName && x.Email == request.Email).FirstOrDefault();
            if (farm == null)
            {
                throw new BadRequestException("Not Found Farm");
            }

            var pondTypes = _unitOfWork.pondTypeRepository.FindByCondition(x=>x.FarmId == farm.FarmId).ToList();

            foreach(var pondType in pondTypes)
            {
               
                var ponds = _unitOfWork.pondRepository.FindByCondition(x => x.PondTypeId == pondType.PondTypeId).ToList();

                //foreach (var pond in ponds)
                //{
                //    var dt = new GetPondAdvanceDTO()
                //    {
                //        pondId = pond.pondId,
                //        pondTypeName = pond.PondTypeName,
                //        originPondId = pond.OriginPondId,
                //       seedId = pond.SeedId,
                //        amountShrimp = pond.AmountShrimp,
                //        deep = pond.Deep,
                //       diameter = pond.Diameter,
                //       status = pond.Status
                //    };

                //    if (pond.OriginPondId == "")
                //    {

                //        dt.StartDate = pond.StartDate;
                //    }
                //    else
                //    {
                //        var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.OriginPondId);
                //        if (originPond == null)
                //        {
                //            throw new BadRequestException("Not Found Pond");
                //        }
                //        dt.StartDate = originPond.StartDate;
                //    }



                //    data.Add(dt);
                //}
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
