using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetMemeber
{
    internal class GetMemeberHandler : IRequestHandler<GetMemeber, List<GetMemeberDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetMemeberDTO> _logger;

        public GetMemeberHandler(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<GetMemeberDTO> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<GetMemeberDTO>> Handle(GetMemeber request, CancellationToken cancellationToken)
        {
            var data = new List<GetMemeberDTO>();
            var members =  _unitOfWork.farmRoleRepository.FindByCondition(x=>x.FarmId == request.FarmId).ToList();

            foreach(var mem in members)
            {
                data.Add(new GetMemeberDTO() {Email = mem.Email, IsAdmin = mem.IsAdmin});
            }
            _logger.LogInformation("Get MemberList successfully");

            return data;
        }
    }
}
