using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;

namespace ShrimpPond.Application.Feature.Farm.Command.CreateFarm
{
    public class CreateFarmHandler : IRequestHandler<CreateFarm, string>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public CreateFarmHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(CreateFarm request, CancellationToken cancellationToken)
        {
            //validate
            var validator = new CreateFarmValidation();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid ET", validatorResult);
            }
            //convert

            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                throw new BadRequestException("User not found");
            }
            var farm = _unitOfWork.farmRepository.FindByCondition(x => x.FarmName == request.FarmName && x.Email == request.Email).FirstOrDefault();
            if(farm != null)
            {
                throw new BadRequestException("Farm is already exit");
            }
            var farmData = new Domain.Farm.Farm()
            {
                FarmName = request.FarmName,
                Address = request.Address,
                Email = user.Email
            };

            _unitOfWork.farmRepository.Add(farmData);
            await _unitOfWork.SaveChangeAsync();
            //return 
            return request.FarmName;
        }
    }
}
