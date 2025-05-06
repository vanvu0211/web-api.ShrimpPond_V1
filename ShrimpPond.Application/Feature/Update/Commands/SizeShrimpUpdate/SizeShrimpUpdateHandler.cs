using MediatR;
using Microsoft.Extensions.Caching.Memory;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate
{
    public class SizeShrimpUpdateHandle : IRequestHandler<SizeShrimpUpdate, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache; // Thêm IMemoryCache

        public SizeShrimpUpdateHandle(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<string> Handle(SizeShrimpUpdate request, CancellationToken cancellationToken)
        {
            //xoa cachekey
            _cache.Remove($"PondInfo_{request.pondId}");
            //validate
            var validator = new SizeShrimpUpdateValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //handler

          
            var pond = _unitOfWork.pondRepository.FindByCondition(x => x.PondId == request.pondId && x.Status == EPondStatus.InActive);

            if (pond.Count() != 0)
            {
                throw new BadRequestException($"Ao {request.pondId} chưa kích hoạt", validatorResult);
            }

            var updatesize = new SizeShrimp()
            {
                PondId = request.pondId,
                SizeValue = request.sizeValue,
                UpdateDate = request.updateDate,
            }
;           _unitOfWork.sizeShrimpRepository.Add(updatesize);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.pondId;
        }
    }
}
