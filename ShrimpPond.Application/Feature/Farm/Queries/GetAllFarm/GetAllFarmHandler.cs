using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm
{
    public class GetAllFarmHandler: IRequestHandler<GetAllFarm,List<FarmDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetAllFarm> _logger;

        public GetAllFarmHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetAllFarm> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public  Task<List<FarmDTO>> Handle(GetAllFarm request, CancellationToken cancellationToken)
        {
            //query
            var data = new List<FarmDTO>();


            var farms = _unitOfWork.farmRepository
                       .FindByCondition(x =>  x.Members.Any(m => m.Email == request.Email))
                       .ToList();

            foreach (var farm in farms)
            {
                var dt = new FarmDTO()
                {
                    farmId= farm.FarmId,
                    farmName = farm.FarmName,
                    address = farm.Address,
                };
                data.Add(dt);
            }
            //logging
            _logger.LogInformation("Get pond successfully");

            return Task.FromResult(data);
        }
    }
}
