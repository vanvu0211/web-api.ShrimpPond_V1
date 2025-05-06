using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
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

        public GetConfigHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Configuration.Configuration> Handle(GetConfig request, CancellationToken cancellationToken)
        {
            var configurations = _unitOfWork.configurationRepository.FindByCondition(x => x.FarmId == request.farmId).OrderBy(x=>x.Id).LastOrDefault();
            return configurations;
        }
    }
}
