using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Feature.PondType.Queries.GetPondType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate
{
    public class GetSizeUpdateHandler : IRequestHandler<GetSizeUpdate, List<GetSizeUpdateDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetSizeUpdate> _logger;

        public GetSizeUpdateHandler(IUnitOfWork unitOfWork, IAppLogger<GetSizeUpdate> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<GetSizeUpdateDTO>> Handle(GetSizeUpdate request, CancellationToken cancellationToken)
        {
            //query
            var sizeShrimps = _unitOfWork.sizeShrimpRepository.FindAll();
            //logging
            _logger.LogInformation("Get sizeShrimps successfully");
            // convert
            var data = _mapper.Map<List<GetSizeUpdateDTO>>(sizeShrimps);
            //return
            return data;
        }
    }
}
