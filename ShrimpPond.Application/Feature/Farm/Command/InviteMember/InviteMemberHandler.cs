using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.GmailService;
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
        private readonly IGmailSender _gmailSender;

        public InviteMemberHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IGmailSender gmailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _gmailSender = gmailSender;
        }

        public async Task<int> Handle(InviteMember request, CancellationToken cancellationToken)
        {
     
            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Không tìm thấy trang trại");
            }
            var user = await _userManager.FindByEmailAsync(request.InviteEmail);
            if (user == null)
            {
                throw new BadRequestException($"Không tìm thấy người dùng với InviteEmail:{request.InviteEmail}");
            }

            var adminFarm = _unitOfWork.farmRepository
                        .FindByCondition(x => x.FarmId == request.FarmId && x.Members.Any(m => m.Email == request.Email && m.Role == Domain.Farm.Role.Member || m.Role == Domain.Farm.Role.Admin))
                        .FirstOrDefault();
            if (adminFarm == null)
            {
                throw new BadRequestException("Bạn không có quyền mời thêm thành viên!");
            }

            var member = new FarmRole()
            {
                FarmId = request.FarmId,
                Email = request.InviteEmail,
                Role = Role.Member,
            };
            //Them thanh vien mới
            _unitOfWork.farmRoleRepository.Add(member);
            //Cập nhật thành viên mới cho trang trại
            farm.Members.Add(member);
            _unitOfWork.farmRepository.Update(farm);
            await _unitOfWork.SaveChangeAsync();

            //Thông báo qua InviteEmail
            await _gmailSender.SendGmail(new Models.Gmail.GmailMessage()
            {
                To = request.InviteEmail,
                Body = $"Bạn đã được chủ trang trại {farm.FarmName} thêm vào danh sách thành viên trang trại!",
                Subject = $"[Thông báo] Thêm thành viên vào trang trại {farm.FarmName}"
            });
            //return 
            return farm.FarmId;
        }
    }
}
