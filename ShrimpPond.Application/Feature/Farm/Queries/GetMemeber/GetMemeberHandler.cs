using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetMemeber
{
    internal class GetMemeberHandler : IRequestHandler<GetMemeber, List<GetMemeberDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache; // Thêm IMemoryCache
        private readonly IAppLogger<GetMemeberDTO> _logger;

        public GetMemeberHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetMemeberDTO> logger, IMemoryCache cache)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<GetMemeberDTO>> Handle(GetMemeber request, CancellationToken cancellationToken)
        {
            // Tạo key cho cache dựa trên pondId
            string cacheKey = $"MemberInfo_{request.FarmId}";

            // Kiểm tra xem dữ liệu đã có trong cache chưa
            if (!_cache.TryGetValue(cacheKey, out List<GetMemeberDTO> data))
            {
                // Nếu không có trong cache, lấy dữ liệu từ DB
                data =  FetchMember(request.FarmId);

                // Lưu vào cache với thời gian sống (ví dụ: 10 phút)
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(5) // Gia hạn nếu được truy cập
                };
                _cache.Set(cacheKey, data, cacheOptions);
            }

            return data;
        }

        private  List<GetMemeberDTO> FetchMember(int farmId)
        {
            var data = new List<GetMemeberDTO>();
            var members = _unitOfWork.farmRoleRepository.FindByCondition(x => x.FarmId == farmId).ToList();

            foreach (var mem in members)
            {
                data.Add(new GetMemeberDTO() { Email = mem.Email, Role = mem.Role });
            }
            _logger.LogInformation("Get MemberList successfully");

            return data;
        }
    }
}
