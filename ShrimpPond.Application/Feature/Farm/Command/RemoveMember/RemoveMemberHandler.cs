using MediatR;
using ShrimpPond.Application.Contract.GmailService;
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
        private readonly IGmailSender _gmailSender;
        public RemoveMemberHandler(IUnitOfWork unitOfWork, IGmailSender gmailSender)
        {
            _unitOfWork = unitOfWork;
            _gmailSender = gmailSender;
        }

        public async Task<int> Handle(RemoveMember request, CancellationToken cancellationToken)
        {

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Không tìm thấy trang trại");
            }

            var member = _unitOfWork.farmRoleRepository.FindByCondition(x=>x.Email ==request.RemoveEmail &&  x.FarmId == request.FarmId).FirstOrDefault();
            if (member == null)
            {
                throw new BadRequestException("Không tìm thấy dùng dùng");
            }

            var adminFarm = _unitOfWork.farmRepository
                        .FindByCondition(x => x.FarmId == request.FarmId && x.Members.Any(m => m.Email == request.Email && m.IsAdmin == true))
                        .FirstOrDefault();
            if (adminFarm == null)
            {
                throw new BadRequestException("Bạn không có quyền xóa thành viên!");
            }

            //Xóa thanh vien 
            _unitOfWork.farmRoleRepository.Remove(member);
            //Cập nhật thành viên mới cho trang trại
            farm.Members.Remove(member);
            _unitOfWork.farmRepository.Update(farm);

            await _unitOfWork.SaveChangeAsync();
            //Thông báo qua email
            await _gmailSender.SendGmail(new Models.Gmail.GmailMessage()
            {
                To = request.RemoveEmail,
                Body = $"Bạn đã được chủ trang trại {farm.FarmName} xóa khỏi danh sách thành viên trang trại!",
                Subject = $"[Thông báo] Loại bỏ thành viên trang trại {farm.FarmName}"
            });
            //return 
            return farm.FarmId;
        }
    }
}
