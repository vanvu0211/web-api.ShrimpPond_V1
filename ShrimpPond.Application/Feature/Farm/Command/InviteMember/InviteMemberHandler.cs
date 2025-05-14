using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.InviteMember
{
    public class InviteMemberHandler: IRequestHandler<InviteMember, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public InviteMemberHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> Handle(InviteMember request, CancellationToken cancellationToken)
        {
     
            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Not found Farm");
            }
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                throw new BadRequestException($"Không tìm thấy người dùng với Email:{request.Email}");
            }
            var member = new FarmRole()
            {
                FarmId = request.FarmId,
                Email = request.Email,
                IsAdmin = false,
            };
            //Them thanh vien mới
            _unitOfWork.farmRoleRepository.Add(member);
            //Cập nhật thành viên mới cho trang trại
            farm.Members.Add(member);
            _unitOfWork.farmRepository.Update(farm);

            await _unitOfWork.SaveChangeAsync();
            //return 
            return farm.FarmId;
        }
    }
}
