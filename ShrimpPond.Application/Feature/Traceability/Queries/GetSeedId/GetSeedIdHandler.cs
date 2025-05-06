using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;


namespace ShrimpPond.Application.Feature.Traceability.Queries.GetSeedId
{
    public class GetSeedIdHandler: IRequestHandler<GetSeedId,List<SeedIdDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetSeedId> _logger;

        public GetSeedIdHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetSeedId> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<SeedIdDTO>> Handle(GetSeedId request, CancellationToken cancellationToken)
        {
            var farm = await _unitOfWork.farmRepository.GetByIdAsync(request.farmId);
            if (farm == null)
            {
                throw new BadRequestException("Farm not found");
            }
            var result = new List<SeedIdDTO>();

            var ponds = _unitOfWork.pondRepository.FindAll();
            foreach (var pond in ponds) 
            {

                if (pond.SeedId == "" || result.Count(x => x.SeedId==pond.SeedId)!=0) continue;

                result.Add(new SeedIdDTO() { SeedId = pond.SeedId });
            }

         
            return result;
        }
    }
}
