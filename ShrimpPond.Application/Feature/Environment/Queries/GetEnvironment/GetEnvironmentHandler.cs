using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Environment.Queries.GetEnvironment
{
    public class GetEnvironmentHandler : IRequestHandler<GetEnvironment, List<GetEnvironmentDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetEnvironment> _logger;

        public GetEnvironmentHandler(IUnitOfWork unitOfWork, IAppLogger<GetEnvironment> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<List<GetEnvironmentDTO>> Handle(GetEnvironment request, CancellationToken cancellationToken)
        {
            //query
            var environments = _unitOfWork.environmentStatusRepository.FindAll();

            //logging
            _logger.LogInformation("Get environment successfully");
            // convert
            var data = _mapper.Map<List<GetEnvironmentDTO>>(environments);
            //return
            return Task.FromResult(data);
        }
    }
}
