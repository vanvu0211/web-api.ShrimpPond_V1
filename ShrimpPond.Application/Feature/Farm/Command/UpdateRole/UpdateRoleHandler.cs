using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.UpdateRole
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRole, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(UpdateRole request, CancellationToken cancellationToken)
        {

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Không tìm thấy trang trại");
            }
            //Kiem tra co ton tai email ko
            var member = _unitOfWork.farmRoleRepository.FindByCondition(x => x.Email == request.UpdateEmail && x.FarmId == request.FarmId).FirstOrDefault();
            if (member == null)
            {
                throw new BadRequestException("Không tìm thấy dùng dùng");
            }

            //Kiem tra co quyen admin ko
            var adminFarm = _unitOfWork.farmRoleRepository.FindByCondition(x => x.Email == request.Email && x.FarmId == request.FarmId && x.Role == Role.Admin || x.Role == Role.Admin).FirstOrDefault();
            if (adminFarm == null)
            {
                throw new BadRequestException("Bạn không có quyền thay đổi quyền thành viên!");
            }
            member.Role = request.Role;
            //Cập nhật quyền
            _unitOfWork.farmRoleRepository.Update(member);
            await _unitOfWork.SaveChangeAsync();
          
            //return 
            return request.Email;
        }
    }
}
