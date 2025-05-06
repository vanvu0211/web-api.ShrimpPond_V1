using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Farm.Command.CreateFarm;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using ShrimpPond.Domain.Machine;

namespace ShrimpPond.Application.Feature.Machine.Command.CreateMachine
{
    public class CreateMachineHandler:IRequestHandler<CreateMachine, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<CreateMachine> _logger;

        public CreateMachineHandler(IUnitOfWork unitOfWork, IAppLogger<CreateMachine> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> Handle(CreateMachine request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateMachineValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert
            List<Domain.Machine.PondId> pondDatas = new List<Domain.Machine.PondId>();
            foreach (var pondId in request.pondIds)
            {
                var pondIdData = new Domain.Machine.PondId() 
                {
                    PondIdForMachine = pondId.pondId,
                    PondName = pondId.pondName
                };
                pondDatas.Add(pondIdData);
                _unitOfWork.pondIdRepository.Add(pondIdData);
            }
            var data = new Domain.Machine.Machine()
            {
                FarmId = request.farmId,
                MachineName = request.machineName,
                PondIds = pondDatas,
                Status = false
            };
            _unitOfWork.machineRepository.Add(data);
            await _unitOfWork.SaveChangeAsync();
            //return 
            _logger.LogInformation("create machine successfully");

            return request.machineName;
        }
    }
}
