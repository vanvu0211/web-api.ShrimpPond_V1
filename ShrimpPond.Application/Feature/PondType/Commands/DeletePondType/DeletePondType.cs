using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.DeletePondType
{
    public class DeletePondType : IRequest<string>
    {
        public string pondTypeId { get; set; } = string.Empty;
    }
}
