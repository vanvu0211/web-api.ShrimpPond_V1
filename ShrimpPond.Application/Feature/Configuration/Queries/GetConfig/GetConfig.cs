using MediatR;
using ShrimpPond.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Configuration.Queries.GetConfig
{
    public class GetConfig: IRequest<Domain.Configuration.Configuration>
    {
        public int FarmId {  get; set; }
    }
}
