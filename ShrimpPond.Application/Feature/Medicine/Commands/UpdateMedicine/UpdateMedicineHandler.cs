using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Food.Commands.UpdateFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.UpdateMedicine
{
    public class UpdateMedicineHandler : IRequestHandler<UpdateMedicine, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMedicineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(UpdateMedicine request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new UpdateMedicineValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
           
            var medicine = await _unitOfWork.medicineRepository.GetByIdAsync(request.medicineId);

            if (medicine == null)
            {
                throw new BadRequestException("Not found Food!");
            }

            medicine.Name = request.newName;

            _unitOfWork.medicineRepository.Update(medicine);
            await _unitOfWork.SaveChangeAsync();
            return request.newName;

        }
    }
}
