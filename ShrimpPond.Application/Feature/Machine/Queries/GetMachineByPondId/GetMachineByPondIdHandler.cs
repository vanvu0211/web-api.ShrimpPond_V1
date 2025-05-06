using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Machine.Queries.GetALlMachine;
namespace ShrimpPond.Application.Feature.Machine.Queries.GetMachineByPondId
{
    public class GetMachineByPondIdHandler: IRequestHandler<GetMachineByPondId, GetMachineByPondIdDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetMachineByPondId> _logger;
        private readonly IMapper _mapper;

        public GetMachineByPondIdHandler(IUnitOfWork unitOfWork, IAppLogger<GetMachineByPondId> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<GetMachineByPondIdDTO> Handle(GetMachineByPondId request, CancellationToken cancellationToken)
        {
            GetMachineByPondIdDTO data = new GetMachineByPondIdDTO();
            List<MachineData> machineDatas = new List<MachineData>();
            var pondIds = _unitOfWork.pondIdRepository.FindByCondition(x => x.PondIdForMachine == request.pondId).ToList();
            foreach(var pondId in pondIds)
            {
                var machine = _unitOfWork.machineRepository.FindByCondition(x => x.MachineId == pondId.MachineId).FirstOrDefault();
                if (machine == null) throw new BadRequestException("Not found machine");
                MachineData machineData = new MachineData()
                {
                    MachineId = machine.MachineId,
                    MachineName = machine.MachineName,
                    MachineStatus = machine.Status
                };
                machineDatas.Add(machineData);
            }

            data.pondId = request.pondId;
            data.machineDatas = machineDatas;

            return data;
        }
    }
}
