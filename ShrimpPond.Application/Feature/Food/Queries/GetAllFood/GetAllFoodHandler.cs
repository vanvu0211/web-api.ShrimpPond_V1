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

namespace ShrimpPond.Application.Feature.Food.Queries.GetAllFood
{
    public class GetAllFoodHandler : IRequestHandler<GetAllFood, List<GetAllFoodDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetAllPond> _logger;

        public GetAllFoodHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetAllPond> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<GetAllFoodDTO>> Handle(GetAllFood request, CancellationToken cancellationToken)
        {
            //query

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var foods = _unitOfWork.foodRepository.FindByCondition(x => x.FarmId == farm.FarmId).ToList();

            //logging
            _logger.LogInformation("Get food successfully");
            // convert
            var data = _mapper.Map<List<GetAllFoodDTO>>(foods);
            //return
            return data;
        }
    }
}
