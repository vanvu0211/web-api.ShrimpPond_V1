using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Pond.Commands.DeletePond
{
    public class DeletePond: IRequest<string>
    {
        public string pondId { get; set; } = string.Empty;
    }
}
