using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.RemoveMember
{
    public class RemoveMemberHandler : IRequestHandler<RemoveMember, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveMemberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(RemoveMember request, CancellationToken cancellationToken)
        {

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Not found Farm");
            }

            var member = _unitOfWork.farmRoleRepository.FindByCondition(x=>x.Email ==request.Email &&  x.FarmId == request.FarmId).FirstOrDefault();
            if (member == null)
            {
                throw new BadRequestException("Not found member");
            }
            //Xóa thanh vien 
            _unitOfWork.farmRoleRepository.Remove(member);
            //Cập nhật thành viên mới cho trang trại
            farm.Members.Remove(member);
            _unitOfWork.farmRepository.Update(farm);

            await _unitOfWork.SaveChangeAsync();
            //return 
            return farm.FarmId;
        }
    }
}
