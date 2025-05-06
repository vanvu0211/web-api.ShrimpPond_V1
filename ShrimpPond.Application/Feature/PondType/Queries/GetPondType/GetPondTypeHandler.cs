using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;


namespace ShrimpPond.Application.Feature.PondType.Queries.GetPondType
{
    public class GetPondTypeHandler: IRequestHandler<GetPondType, List<PondTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetPondType> _logger;

        public GetPondTypeHandler(IUnitOfWork unitOfWork, IAppLogger<GetPondType> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<List<PondTypeDto>> Handle(GetPondType request, CancellationToken cancellationToken)
        {
            //query
            var pondTypes = _unitOfWork.pondTypeRepository.FindAll();
            //logging
            _logger.LogInformation("Get location successfully");
            // convert
            var data = _mapper.Map<List<PondTypeDto>>(pondTypes);
            //return
            return Task.FromResult(data);
        }
    }
}
