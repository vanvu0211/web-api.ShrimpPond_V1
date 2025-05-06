using System.Globalization;
using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;

namespace ShrimpPond.Application.Feature.Traceability.Queries.GetTraceability
{
    public class GetTraceabilityHandler: IRequestHandler<GetTraceability,TraceabilityDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetTraceability> _logger;

        public GetTraceabilityHandler(IUnitOfWork unitOfWork, IAppLogger<GetTraceability> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<TraceabilityDTO> Handle(GetTraceability request, CancellationToken cancellationToken)
        {
            //query


            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            TraceabilityDTO traceabilitie = new();

            var harvestPonds = _unitOfWork.harvestRepository.FindByCondition(x=>x.SeedId == request.seedId && x.HarvestTime == request.harvestTime ).ToList();

            if(harvestPonds.Count == 0)
            {
                throw new BadRequestException("Not found");
            }

            var harvestPondIds = new List<string>();
            var harvestSizes = new List<string>();
            var endDate = DateTime.Now;
            float size = 0;

            foreach (var harvestPond in harvestPonds)
            {
                var pond = _unitOfWork.pondRepository.FindByCondition(p => p.PondId == harvestPond.PondId ).FirstOrDefault();
                if(pond == null)
                {
                    throw new BadRequestException("Not found Pond");
                }
                //Nếu có ao gốc
                if(pond.OriginPondId != "")
                {
                    var originPond = _unitOfWork.pondRepository.FindByCondition(p=>p.PondId==pond.OriginPondId).FirstOrDefault();
                    if(originPond != null)
                    {
                        endDate= originPond.StartDate;
                    } 
                } 
                else endDate = pond.StartDate;
                traceabilitie.DaysOfRearing =(harvestPond.HarvestDate - endDate).Days;

                //Lấy trung bình size tôm
                harvestSizes.Add(pond.PondName + "-" + harvestPond.Size.ToString(CultureInfo.InvariantCulture));
                size += harvestPond.Size;

                //tổng số lượng tôm các ao
                traceabilitie.TotalAmount += harvestPond.Amount;

                //Danh sách ao thu hoạch
                harvestPondIds.Add((pond.OriginPondId == null)? "-" + pond.PondName : "" + pond.PondName);

                //Danh sách giấy xét nghiệm
                var certificates =  _unitOfWork.certificateRepository.FindByCondition(x=>x.PondId == harvestPond.PondId && x.CertificateName == $"Giấy xét nghiệm kháng sinh lần thứ {request.harvestTime}").ToList();

                foreach (var certificate in certificates)
                {
                    if (certificate.FileData != null) traceabilitie.Certificates.Add(certificate.FileData);
                }
        
                traceabilitie.FarmName = farm.FarmName;

                
                traceabilitie.Address = farm.Address;
            }
            traceabilitie.SeedId = request.seedId;
            traceabilitie.HarvestTime = request.harvestTime;
            
            //Tạo mã ao thu hoạch theo đúng format
            foreach(var harvestPondId in harvestPondIds)
            {
                traceabilitie.HarvestPondId += harvestPondId + ";";
            }

            if (traceabilitie.HarvestPondId.EndsWith(";"))
            {
                traceabilitie.HarvestPondId =  traceabilitie.HarvestPondId.Remove(traceabilitie.HarvestPondId.Length - 1);
            }

            //Tạo size theo format

            foreach (var harvestSize in harvestSizes)
            {
                traceabilitie.Size += harvestSize + ";";
            }

            if (traceabilitie.Size.EndsWith(";"))
            {
                traceabilitie.Size = traceabilitie.Size.Remove(traceabilitie.Size.Length - 1);
            }

            traceabilitie.Size =  $"[{(size / harvestPonds.Count)}]" +"["+ traceabilitie.Size+"]";

            _logger.LogInformation("Get traceabilitie successfully");

            
            //return
            return traceabilitie;
        }
    }
}
