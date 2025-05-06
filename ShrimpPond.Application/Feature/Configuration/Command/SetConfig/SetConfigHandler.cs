using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Configuration.Command.SetConfig
{
    public class SetConfigHandler: IRequestHandler<SetConfig, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SetConfigHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(SetConfig request, CancellationToken cancellationToken)
        {
            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }

            var newConfig = new Domain.Configuration.Configuration()
            {
                Id = new Guid(),
                pHLow = request.pHLow,
                pHTop = request.pHTop,
                oxiLow = request.oxiLow,
                oxiTop = request.oxiTop,
                temperatureLow = request.temperatureLow,
                temperatureTop = request.temperatureTop,
                FarmId = request.FarmId,
            };
            _unitOfWork.configurationRepository.Add(newConfig);
            await _unitOfWork.SaveChangeAsync();
            return newConfig.Id;
        }

    }
}
