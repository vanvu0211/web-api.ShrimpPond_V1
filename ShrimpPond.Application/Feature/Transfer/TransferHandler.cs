using MediatR;
using Microsoft.Extensions.Hosting;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Transfer
{
    public class TransferHandler: IRequestHandler<Transfer,string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(Transfer request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new TransferValidation();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //handler
            var tranfserPond = await _unitOfWork.pondRepository.GetByIdAsync(request.transferPondId);
            if (tranfserPond == null)
            {
                throw new BadRequestException("Not found TranfserPond");
            }

            var originPond = _unitOfWork.pondRepository.FindByCondition(x => x.PondId == request.originPondId && x.Status == EPondStatus.Active ).FirstOrDefault();
            if (originPond == null )
            {
                throw new BadRequestException("Not found OriginPond");
            }
            //chuyen du lieu sang ao moi
            tranfserPond.Status = EPondStatus.Active;
            tranfserPond.OriginPondId = originPond.PondId;
            tranfserPond.SeedId = originPond.SeedId;
            tranfserPond.SeedName = originPond.SeedName;
            tranfserPond.AmountShrimp = request.amount;
            tranfserPond.StartDate = originPond.StartDate;
            _unitOfWork.pondRepository.Update(tranfserPond);

            // Du lieu moi truong
            var environmentDatas = _unitOfWork.environmentStatusRepository.FindByCondition(x => x.PondId == originPond.PondId).ToList();
            foreach (var environmentData in environmentDatas)
            {
                var data = new Domain.Environments.EnvironmentStatus()
                {
                    PondId = request.transferPondId,
                    Value = environmentData.Value,
                    Name = environmentData.Name,
                    Timestamp = environmentData.Timestamp
                };
                _unitOfWork.environmentStatusRepository.Add(data);
            }
            // Size tôm
            var sizeShrimps = _unitOfWork.sizeShrimpRepository.FindByCondition(x => x.PondId == request.originPondId).ToList();
            foreach (var sizeSrimp in sizeShrimps)
            {
                var data = new SizeShrimp()
                {
                    PondId = request.transferPondId,
                    SizeValue = sizeSrimp.SizeValue,
                    UpdateDate = sizeSrimp.UpdateDate
                };
                _unitOfWork.sizeShrimpRepository.Add(data);
            }
            //// Lich su cho an
            //var feedings = _unitOfWork.foodFeedingRepository.FindByCondition(x => x.pondId == request.originPondId).ToList();
            //foreach (var feeding in feedings)
            //{
            //    var data = new Domain.Food.FoodFeeding()
            //    {

            //    };
            //    _unitOfWork.foodFeedingRepository.Update(feeding);
            //}
            ////Lich su dieu tri
            //var treatments = _unitOfWork.medicineFeedingRepository.FindByCondition(x => x.pondId == request.originPondId).ToList();
            //foreach (var treatment in treatments)
            //{
            //    treatment.pondId = request.transferPondId;
            //    _unitOfWork.medicineFeedingRepository.Update(treatment);
            //}
            //Lich su tom hao
            var lossShrimps = _unitOfWork.lossShrimpRepository.FindByCondition(x => x.PondId == request.originPondId).ToList();
            foreach (var lossShrimp in lossShrimps)
            {
                var data = new LossShrimp()
                {
                    PondId = request.transferPondId,
                    LossValue = lossShrimp.LossValue,
                    UpdateDate = lossShrimp.UpdateDate
                };
                _unitOfWork.lossShrimpRepository.Add(data);
            }

            await _unitOfWork.SaveChangeAsync();

            return "ok";
        }
    }
}
