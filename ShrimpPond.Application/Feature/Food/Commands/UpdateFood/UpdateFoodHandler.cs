using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Food.Commands.CreateNewFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.UpdateFood
{
    public class UpdateFoodHandler : IRequestHandler<UpdateFood, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateFoodHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(UpdateFood request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new UpdateFoodValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }

            var food =  await _unitOfWork.foodRepository.GetByIdAsync(request.foodId);
            if (food == null )
            {
                throw new BadRequestException("Not found Food!");
            }

            food.Name = request.newName;

            _unitOfWork.foodRepository.Update(food);
            await _unitOfWork.SaveChangeAsync();
            return request.newName;

        }
    }
}
