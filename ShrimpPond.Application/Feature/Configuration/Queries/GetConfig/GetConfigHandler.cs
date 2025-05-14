using MediatR;
using Microsoft.AspNetCore.Identity;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Configuration.Command.SetConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Configuration.Queries.GetConfig
{
    public class GetConfigHandler : IRequestHandler<GetConfig, Domain.Configuration.Configuration>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public GetConfigHandler(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Domain.Configuration.Configuration> Handle(GetConfig request, CancellationToken cancellationToken)
        {
            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.FarmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }

            var configurations = _unitOfWork.configurationRepository.FindByCondition(x => x.FarmId == request.FarmId).OrderBy(x=>x.Id).LastOrDefault();
            return configurations;
        }
    }
}
