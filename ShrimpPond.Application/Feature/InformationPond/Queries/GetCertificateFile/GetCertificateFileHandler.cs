using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Pond.Queries.GetHarvestTime;
namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetCertificateFile
{
    public class GetCertificateFileHandler: IRequestHandler<GetCertificateFile,GetCertificateFileDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCertificateFileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCertificateFileDTO> Handle(GetCertificateFile request, CancellationToken cancellationToken)
        {

            var certificateWithFile = _unitOfWork.certificateRepository
                                        .FindByCondition(x => x.CertificateId == request.certificateId)
                                        .Select(x => x.FileData)
                                        .FirstOrDefault();
            if(certificateWithFile == null)
            {
                throw new BadRequestException("Not fond file");
            }
            var data = new GetCertificateFileDTO()
            {
                fileData = certificateWithFile
            };

            return data;
        }
    }
}
