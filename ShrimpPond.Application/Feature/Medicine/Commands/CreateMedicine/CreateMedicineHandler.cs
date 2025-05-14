using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.CreateMedicine
{
    public class CreateMedicineHandler : IRequestHandler<CreateMedicine, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMedicineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateMedicine request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateMedicineValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle


            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var condition = _unitOfWork.medicineRepository.FindByCondition(x => x.Name == request.name && x.FarmId == farm.FarmId).FirstOrDefault();
            if (condition != null)
            {
                throw new BadRequestException("Thuốc đã tồn tại!", validatorResult);
            }

            var newmedicine = new Domain.Medicine.Medicine()
            {
                Name = request.name,
                FarmId = farm.FarmId
            };

            _unitOfWork.medicineRepository.Add(newmedicine);
            await _unitOfWork.SaveChangeAsync();
            return request.name;
        }
    }
}
