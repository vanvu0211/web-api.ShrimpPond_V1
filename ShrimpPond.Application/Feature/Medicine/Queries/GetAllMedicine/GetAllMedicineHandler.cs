using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Food.Queries.GetAllFood;
using ShrimpPond.Application.Feature.Pond.Queries.GetAllPond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine
{
    public class GetAllMedicineHandler : IRequestHandler<GetAllMedicine, List<GetAllMedicineDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetAllMedicine> _logger;

        public GetAllMedicineHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetAllMedicine> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<GetAllMedicineDTO>> Handle(GetAllMedicine request, CancellationToken cancellationToken)
        {
            //query

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var medicines = _unitOfWork.medicineRepository.FindByCondition(x => x.FarmId == farm.FarmId).ToList();

            //logging
            _logger.LogInformation("Get medicine successfully");
            // convert
            var data = _mapper.Map<List<GetAllMedicineDTO>>(medicines);
            //return
            return data;
        }
    }
}
