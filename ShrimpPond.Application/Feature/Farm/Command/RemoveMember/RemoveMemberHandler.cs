﻿using MediatR;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _cache; // Thêm IMemoryCache
        private readonly IGmailSender _gmailSender;
        public RemoveMemberHandler(IUnitOfWork unitOfWork, IGmailSender gmailSender, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _gmailSender = gmailSender;
            _cache = cache;
        }

        public async Task<int> Handle(RemoveMember request, CancellationToken cancellationToken)
        {

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Không tìm thấy trang trại");
            }
            //Kiem tra co ton tai email ko
            var member = _unitOfWork.farmRoleRepository.FindByCondition(x => x.Email == request.RemoveEmail && x.FarmId == request.FarmId).FirstOrDefault();
            if (member == null)
            {
                throw new BadRequestException("Không tìm thấy dùng dùng");
            }
            //kiem tra co dang xoa Manager ko
            if(member.Role == Role.Admin)
            {
                throw new BadRequestException("Bạn không có quyền xóa thành viên");
            }

            //Kiem tra co quyen admin ko
            var adminFarm = _unitOfWork.farmRoleRepository.FindByCondition(x => x.Email == request.Email && x.FarmId == request.FarmId && x.Role == Role.Member || x.Role == Role.Admin).FirstOrDefault();
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
            await _gmailSender.SendNotificationEmailAsync(new Models.Gmail.GmailMessage()
            {
                To = request.RemoveEmail,
                Body = $"Bạn đã được chủ trang trại {farm.FarmName} xóa khỏi danh sách thành viên trang trại!",
                Subject = $"[Thông báo] Loại bỏ thành viên trang trại {farm.FarmName}"
            });
            //Xoa bo nho dem

            _cache.Remove($"MemberInfo_{request.FarmId}");
            //return 
            return farm.FarmId;
        }
    }
}
