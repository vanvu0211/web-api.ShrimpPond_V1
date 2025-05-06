using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Domain.Medicine;
using Microsoft.Extensions.Caching.Memory;

namespace ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding
{
    internal class MedicineFeedingHandler : IRequestHandler<MedicineFeeding, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache; // Thêm IMemoryCache

        public MedicineFeedingHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }
        public async Task<string> Handle(MedicineFeeding request, CancellationToken cancellationToken)
        {
            //xoa cachekey
            _cache.Remove($"PondInfo_{request.pondId}");
            //validate
            var validator = new MedicineFeedingValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var pond = _unitOfWork.pondRepository.FindByCondition(x => x.PondId == request.pondId && x.Status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.pondId} chưa kích hoạt", validatorResult);
            }

            List<MedicineForFeeding> medicineFeedings = new List<MedicineForFeeding>();

            if (request.medicines != null)
            {
                foreach (var medicine in request.medicines)
                {
                    var medicinefeeding = new MedicineForFeeding()
                    {
                        Name = medicine.name,
                        Amount = medicine.amount
                    };
                    medicineFeedings.Add(medicinefeeding);
                }
            }

            var feeding = new Domain.Medicine.MedicineFeeding()
            {
                PondId = request.pondId,
                FeedingDate = request.feedingDate,
                Medicines = medicineFeedings
            };

            _unitOfWork.medicineFeedingRepository.Add(feeding);
            await _unitOfWork.SaveChangeAsync();
            return request.pondId;
        }
    }
}
