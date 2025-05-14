using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Pond.Commands.DeletePond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.DeleteFarm
{
    public class DeleteFarmHandler: IRequestHandler<DeleteFarm,int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFarmHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteFarm request, CancellationToken cancellationToken)
        {

            //validate
            var deleteFarm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (deleteFarm == null)
            {
                throw new BadRequestException("Không tìm thấy trang trại!");
            }

            var adminFarm = _unitOfWork.farmRepository
                        .FindByCondition(x => x.FarmId == request.FarmId && x.Members.Any(m => m.Email == request.Email && m.Role == Domain.Farm.Role.Admin || m.Role == Domain.Farm.Role.Admin))
                        .FirstOrDefault();
            if (adminFarm == null)
            {
                throw new BadRequestException("Bạn không có quyền xóa trang trại này!");
            }

            var ponds = _unitOfWork.pondRepository.FindByCondition(x => x.FarmId == deleteFarm.FarmId).ToList();
            if (ponds.Count !=  0)
            {     
                throw new BadRequestException($"Trang trại còn các ao: {string.Join(",", ponds.Select(pond => pond.PondName).OrderBy(name => name))}!");
            }

            //Tìm danh sách role chứa farmId
            var roles = _unitOfWork.farmRoleRepository.FindByCondition(x => x.FarmId == deleteFarm.FarmId).ToList();
            _unitOfWork.farmRoleRepository.RemoveRange(roles);
            _unitOfWork.farmRepository.Remove(deleteFarm);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return deleteFarm.FarmId;
        }
    }
}
