using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.CleanSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.CreateCleanTime
{
    public class CreateCleanTimeHandler : IRequestHandler<CreateCleanTime, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCleanTimeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateCleanTime request, CancellationToken cancellationToken)
        {

            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Not Found Farm");
            }
            var cleanTime = request.cleanTime;
            var data = new CleanSensor() 
            { 
                CleanTime = cleanTime ,
                FarmId = farm.FarmId,

              
            };
            _unitOfWork.cleanSensorRepository.Add(data);
            await _unitOfWork.SaveChangeAsync();
            return "Sucessfully!";
        }
    }
}
