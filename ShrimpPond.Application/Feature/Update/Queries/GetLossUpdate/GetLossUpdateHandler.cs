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

namespace ShrimpPond.Application.Feature.Update.Queries.GetLossUpdate
{
    public class GetLossUpdateHandler : IRequestHandler<GetLossUpdate, List<GetLossUpdateDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetLossUpdate> _logger;

        public GetLossUpdateHandler(IUnitOfWork unitOfWork, IAppLogger<GetLossUpdate> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<GetLossUpdateDTO>> Handle(GetLossUpdate request, CancellationToken cancellationToken)
        {
            //query
            var lossShrimps = _unitOfWork.lossShrimpRepository.FindAll();
            //logging
            _logger.LogInformation("Get lossShrimps successfully");
            // convert
            var data = _mapper.Map<List<GetLossUpdateDTO>>(lossShrimps);
            //return
            return data;
        }
    }
}
