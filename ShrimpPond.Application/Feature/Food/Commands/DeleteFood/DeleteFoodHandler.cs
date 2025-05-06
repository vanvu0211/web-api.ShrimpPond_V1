using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.PondType.Commands.DeletePondType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.DeleteFood
{
    public class DeleteFoodHandler : IRequestHandler<DeleteFood, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFoodHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteFood request, CancellationToken cancellationToken)
        {

            //validate
           
            var deletefood = await _unitOfWork.foodRepository.GetByIdAsync(request.foodId);

            if (deletefood == null)
            {
                throw new BadRequestException("Not found PondType");
            }

            _unitOfWork.foodRepository.Remove(deletefood);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return deletefood.Name;
        }
    }
}
