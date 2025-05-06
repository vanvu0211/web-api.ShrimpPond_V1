using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding
{
    public class GetMedicineFeedingHandler : IRequestHandler<GetMedicineFeeding, List<GetMedicineFeedingDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetMedicineFeeding> _logger;

        public GetMedicineFeedingHandler(IUnitOfWork unitOfWork, IAppLogger<GetMedicineFeeding> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<GetMedicineFeedingDTO>> Handle(GetMedicineFeeding request, CancellationToken cancellationToken)
        {
            //query

            var medicineFeedings = _unitOfWork.medicineFeedingRepository.FindByCondition(f => f.PondId == request.pondId);
            List<GetMedicineFeedingDTO> getMedicineFeedingDTOs = new List<GetMedicineFeedingDTO>();
            List<Medicines> medicines = new List<Medicines>();
            foreach (var medicineFeeding in medicineFeedings)
            {
                var medicineforFeedings = _unitOfWork.medicineForFeedingRepository.FindAll().Where(f => f.MedicineFeedingId == medicineFeeding.MedicineFeedingId).ToList();
                foreach (var medicineforFeeding in medicineforFeedings)
                {
                    var medicine = new Medicines()
                    {
                        name = medicineforFeeding.Name,
                        amount = medicineforFeeding.Amount,
                    };
                    medicines.Add(medicine);
                }

                var getMedicineFeedingDTO = new GetMedicineFeedingDTO()
                {
                    pondId = request.pondId,
                    feedingDate = medicineFeeding.FeedingDate,
                    medicines = medicines
                };
                getMedicineFeedingDTOs.Add(getMedicineFeedingDTO);
                medicines = new();
            }

            //logging
            _logger.LogInformation("Get sizeShrimps successfully");
            // convert

            return getMedicineFeedingDTOs;
        }
    }
}
