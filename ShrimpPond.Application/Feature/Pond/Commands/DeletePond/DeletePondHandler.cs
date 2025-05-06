using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Commands.DeletePondType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.DeletePond
{
    public class DeletePondHandler: IRequestHandler<DeletePond,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePondHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeletePond request, CancellationToken cancellationToken)
        {

            //validate
            var deletePond = await _unitOfWork.pondRepository.GetByIdAsync(request.pondId);
            if (deletePond == null)
            {
                throw new BadRequestException("Not found Pond");
            }

            //Xóa danh sách cho ăn 
            var foodfeedings = _unitOfWork.foodFeedingRepository.FindByCondition(x => x.PondId == request.pondId).ToList();
            _unitOfWork.foodFeedingRepository.RemoveRange(foodfeedings);

            foreach (var foodfeeding in foodfeedings)
            {
                var foodforfeedings = _unitOfWork.foodForFeedingRepository.FindByCondition(x => x.FoodFeedingId == foodfeeding.FoodFeedingId);
                _unitOfWork.foodForFeedingRepository.RemoveRange(foodforfeedings);
            }
            //Xóa danh sách điều trị
            var medicinefeedings = _unitOfWork.medicineFeedingRepository.FindByCondition(x => x.PondId == request.pondId).ToList(); ;
            _unitOfWork.medicineFeedingRepository.RemoveRange(medicinefeedings);

            foreach (var medicinefeeding in medicinefeedings)
            {
                var medicineforfeedings = _unitOfWork.medicineForFeedingRepository.FindByCondition(x => x.MedicineFeedingId == medicinefeeding.MedicineFeedingId);
                _unitOfWork.medicineForFeedingRepository.RemoveRange(medicineforfeedings);
            }

            //Xóa danh sách lấy xét nghiem
            var certificates = _unitOfWork.certificateRepository.FindByCondition(x => x.PondId == deletePond.PondId).ToList();
            _unitOfWork.certificateRepository.RemoveRange(certificates);

            //Xóa danh sách thu hoạch
            var harvests = _unitOfWork.harvestRepository.FindByCondition(x => x.PondId == deletePond.PondId).ToList();
            _unitOfWork.harvestRepository.RemoveRange(harvests);

            //Xoa du lieu size tom
            var sizeShrimps = _unitOfWork.sizeShrimpRepository.FindByCondition(x => x.PondId == deletePond.PondId).ToList();
            _unitOfWork.sizeShrimpRepository.RemoveRange(sizeShrimps);
            //Xóa ao
            _unitOfWork.pondRepository.Remove(deletePond);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.pondId;
        }

    }
}
