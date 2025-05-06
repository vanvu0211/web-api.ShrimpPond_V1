using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Machine.Command.CreateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Machine.Command.UpdateMachine
{
    public class UpdateMachineHandler : IRequestHandler<UpdateMachine, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<UpdateMachine> _logger;

        public UpdateMachineHandler(IUnitOfWork unitOfWork, IAppLogger<UpdateMachine> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> Handle(UpdateMachine request, CancellationToken cancellationToken)
        {
            //Tim may
            var machine = await _unitOfWork.machineRepository.GetByIdAsync(request.machineId);
            if (machine == null) throw new BadRequestException("Not found machine");
            //Lay danh sach ao
            List<Domain.Machine.PondId> pondIds = new List<Domain.Machine.PondId>();
            //Xoa danh sach ao truoc do
            var ponds = _unitOfWork.pondIdRepository.FindByCondition(x => x.MachineId == request.machineId).ToList();
            _unitOfWork.pondIdRepository.RemoveRange(ponds);
            foreach (var pondId in request.pondIds)
            {
                pondIds.Add(new Domain.Machine.PondId() 
                { 
                    PondIdForMachine = pondId.pondId, 
                    PondName = pondId.pondName 
                });
            }
            //Cap nhat vao may
            machine.PondIds = pondIds;
            _unitOfWork.machineRepository.Update(machine);
            await _unitOfWork.SaveChangeAsync();
            //return 
            _logger.LogInformation("Update machine successfully");

            return request.machineId.ToString();
        }
    }
}
