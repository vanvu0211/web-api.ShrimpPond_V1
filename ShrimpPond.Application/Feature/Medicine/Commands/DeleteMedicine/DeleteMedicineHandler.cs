using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Food.Commands.DeleteFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.DeleteMedicine
{
    internal class DeleteMedicineHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteMedicineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteMedicine request, CancellationToken cancellationToken)
        {

            //validate
           
            var deletemedicine = await _unitOfWork.medicineRepository.GetByIdAsync(request.medicineId);


            if (deletemedicine == null)
            {
                throw new BadRequestException("Not found PondType");
            }



            _unitOfWork.medicineRepository.Remove(deletemedicine);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return deletemedicine.Name;
        }
    }
}
