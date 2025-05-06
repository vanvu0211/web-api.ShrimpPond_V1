using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.InformationPond.Queries.GetCertificateFile
{
    public class GetCertificateFile: IRequest<GetCertificateFileDTO>
    {
        public int certificateId { get; set; }
    }
}
