using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Commands.CreatePondType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.DeletePondType
{
    public class DeletePondTypeHandler: IRequestHandler<DeletePondType,string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePondTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeletePondType request, CancellationToken cancellationToken)
        {

            //validate

            var deletePondType = await _unitOfWork.pondTypeRepository.GetByIdAsync(request.pondTypeId);
            
            if(deletePondType == null)
            {
                throw new BadRequestException("Not found PondType");
            }

            var pond = _unitOfWork.pondRepository.FindByCondition(x => x.PondTypeId == deletePondType.PondTypeId).ToList();


            if (pond.Count() !=0)
            {
                throw new BadRequestException("Pond still on list");
            }

            _unitOfWork.pondTypeRepository.Remove(deletePondType);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.pondTypeId;
        }
    }
}
