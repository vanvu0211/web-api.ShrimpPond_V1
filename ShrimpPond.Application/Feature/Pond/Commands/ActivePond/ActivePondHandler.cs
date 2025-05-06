using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.Application.Feature.Pond.Commands.ActivePond
{
    public class ActivePondHandler: IRequestHandler<NurseryPond.Commands.ActiveNurseryPond.ActivePond, string>
    
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        public ActivePondHandler(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<string> Handle(NurseryPond.Commands.ActiveNurseryPond.ActivePond request, CancellationToken cancellationToken)
        {
            //xoa cachekey
            _cache.Remove($"PondInfo_{request.pondId}");
            //validate
            var validator = new ActivePondValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //Handle

            var activePond = await _unitOfWork.pondRepository.GetByIdAsync(request.pondId);
            if(activePond == null)
            {
                throw new BadRequestException("Not found Pond");
            }

            var sizeShrimp = new Domain.PondData.SizeShrimp()
            {
                PondId = request.pondId,
                SizeValue = request.sizeShrimp,
                UpdateDate = DateTime.Now,
            };

            _unitOfWork.sizeShrimpRepository.Add(sizeShrimp);
            //Active
            activePond.StartDate = DateTime.Now;
            activePond.Status = EPondStatus.Active;
            activePond.AmountShrimp = request.amountShrimp;
            //activePond.UnitAmountShrimp = request.UnitAmountShrimp;
            activePond.OriginPondId = request.originPondId;
            if (request.certificates != null)
            {
                foreach (var pic in request.certificates)
                {
                    var certificate= new  Domain.PondData.Certificate()
                    {
                        CertificateName = "Giấy xét nghiệm tôm thương phẩm",
                        PondId = request.pondId,
                        FileData = Convert.FromBase64String(pic),
                    };
                    _unitOfWork.certificateRepository.Add(certificate);
                }
            }
            activePond.SeedId = request.seedId;
            activePond.SeedName = request.seedName;

            _unitOfWork.pondRepository.Update(activePond);
            await _unitOfWork.SaveChangeAsync();
            return  request.pondId;
        }
    }
}
