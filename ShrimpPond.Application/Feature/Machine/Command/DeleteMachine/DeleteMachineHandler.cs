using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Machine.Command.CreateMachine;
namespace ShrimpPond.Application.Feature.Machine.Command.DeleteMachine
{
    public class DeleteMachineHandler: IRequestHandler<DeleteMachine,int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMachineHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteMachine request, CancellationToken cancellationToken)
        {
            //tim
            var deleteMachine = await _unitOfWork.machineRepository.GetByIdAsync(request.machineId);
            if (deleteMachine is null) throw new BadRequestException("Not found machine");
            //Xoa
            _unitOfWork.machineRepository.Remove(deleteMachine);
            await _unitOfWork.SaveChangeAsync();
            return request.machineId;
        }
    }


}
