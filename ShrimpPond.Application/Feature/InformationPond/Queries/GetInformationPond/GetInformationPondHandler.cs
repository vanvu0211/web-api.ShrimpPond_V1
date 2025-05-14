using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory; // Thêm namespace cho cache
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Feeding.Commands.MedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate;
using ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetInformationPond
{
    public class GetInformationPondHandler : IRequestHandler<GetInformationPond, GetInformationPondDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache; // Thêm IMemoryCache

        public GetInformationPondHandler(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetInformationPondDTO> Handle(GetInformationPond request, CancellationToken cancellationToken)
        {
            // Tạo key cho cache dựa trên pondId
            string cacheKey = $"PondInfo_{request.pondId}";

            // Kiểm tra xem dữ liệu đã có trong cache chưa
            if (!_cache.TryGetValue(cacheKey, out GetInformationPondDTO data))
            {
                // Nếu không có trong cache, lấy dữ liệu từ DB
                data = await FetchPondData(request.pondId);

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

        private async Task<GetInformationPondDTO> FetchPondData(string pondId)
        {
            GetInformationPondDTO data = new GetInformationPondDTO();
            var pond = await _unitOfWork.pondRepository.GetByIdAsync(pondId);
            if (pond == null)
            {
                throw new BadRequestException("Không tìm thấy ao");
            }
            var originPond = await _unitOfWork.pondRepository.GetByIdAsync(pond.OriginPondId);
            var pondType = _unitOfWork.pondTypeRepository.FindByCondition(x => x.PondTypeId == pond.PondTypeId).FirstOrDefault();
            if (pondType == null)
            {
                throw new BadRequestException("Không tìm thấy loại ao");
            }

            // Thông tin ao cơ bản
            data.pondId = pond.PondId;
            data.pondName = pond.PondName;
            data.deep = pond.Deep;
            data.diameter = pond.Diameter;
            data.pondTypeId = pond.PondTypeId;
            data.pondTypeName = pondType.PondTypeName;
            data.status = (pond.Status == EPondStatus.Active) ? "Đã kích hoạt" : "Chưa kích hoạt";
            data.originPondName = (originPond == null) ? "Không có ao gốc" : originPond.PondName;
            data.seedId = pond.SeedId;
            data.seedName = pond.SeedName;
            data.startDate = pond.StartDate;

            // Thông tin về giấy chứng nhận

            //var certificates = _unitOfWork.certificateRepository
            //                    .FindByCondition(x => x.pondId == pond.pondId)
            //                    .Select(x => new { x.certificateId, x.certificateName, x.pondId })
            //                    .ToList();
            //foreach (var certificate in certificates)
            //{
            //    data.certificates.Add(new GetCertificates()
            //    {
            //        certificateId = certificate.certificateId
            //    });
            //}
            var certificates = _unitOfWork.certificateRepository.FindByCondition(x => x.PondId == pond.PondId).ToList();
            data.certificates = certificates;
                // Thông tin về cho ăn
            var foodFeedings = _unitOfWork.foodFeedingRepository.FindByCondition(f => f.PondId == pondId);
            List<GetFoodFeedingDTO> getFoodFeedingDTOs = new List<GetFoodFeedingDTO>();
            List<Foods> foods = new List<Foods>();
            foreach (var foodFeeding in foodFeedings)
            {
                var foodforFeedings = _unitOfWork.foodForFeedingRepository.FindAll()
                    .Where(f => f.FoodFeedingId == foodFeeding.FoodFeedingId).ToList();
                foreach (var foodforFeeding in foodforFeedings)
                {
                    var food = new Foods()
                    {
                        name = foodforFeeding.Name,
                        amount = foodforFeeding.Amount,
                    };
                    foods.Add(food);
                }

                var getFoodFeedingDTO = new GetFoodFeedingDTO()
                {
                    PondId = pondId,
                    FeedingDate = foodFeeding.FeedingDate,
                    Foods = foods
                };
                getFoodFeedingDTOs.Add(getFoodFeedingDTO);
                foods = new();
            }
            data.feedingFoods = getFoodFeedingDTOs;

            // Thông tin về điều trị
            var medicineFeedings = _unitOfWork.medicineFeedingRepository.FindByCondition(f => f.PondId == pondId);
            List<GetMedicineFeedingDTO> getMedicineFeedingDTOs = new List<GetMedicineFeedingDTO>();
            List<Medicines> medicines = new List<Medicines>();
            foreach (var medicineFeeding in medicineFeedings)
            {
                var medicineforFeedings = _unitOfWork.medicineForFeedingRepository.FindAll()
                    .Where(f => f.MedicineFeedingId == medicineFeeding.MedicineFeedingId).ToList();
                foreach (var medicineforFeeding in medicineforFeedings)
                {
                    var medicine = new Medicines()
                    {
                        name = medicineforFeeding.Name,
                        amount = medicineforFeeding.Amount,
                    };
                    medicines.Add(medicine);
                }

                var getMedicineFeedingDTO = new GetMedicineFeedingDTO()
                {
                    pondId = pondId,
                    feedingDate = medicineFeeding.FeedingDate,
                    medicines = medicines
                };
                getMedicineFeedingDTOs.Add(getMedicineFeedingDTO);
                medicines = new();
            }
            data.feedingMedicines = getMedicineFeedingDTOs;

            // Lấy thông tin kích thước tôm
            var sizeShrimps = _unitOfWork.sizeShrimpRepository.FindByCondition(x => x.PondId == pond.PondId);
            data.sizeShrimps = _mapper.Map<List<GetSizeUpdateDTO>>(sizeShrimps);

            // Lấy dữ liệu tôm hao
            var lossShrimps = _unitOfWork.lossShrimpRepository.FindByCondition(x => x.PondId == pond.PondId);
            data.lossShrimps = _mapper.Map<List<GetLossUpdateDTO>>(lossShrimps);

            // Lấy dữ liệu thu hoạch
            var harvestData = _unitOfWork.harvestRepository.FindByCondition(x => x.PondId == pond.PondId);
            data.harvests = _mapper.Map<List<HarvestDTO>>(harvestData);

            return data;
        }
    }
}