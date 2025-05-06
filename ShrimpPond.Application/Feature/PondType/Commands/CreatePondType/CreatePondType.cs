using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.PondType.Commands.CreatePondType
{
    public class CreatePondType:IRequest<string>
    {
        public string pondTypeId { get; set; } = string.Empty;
        public string pondTypeName {  get; set; } = string.Empty;
        public int farmId { get; set; }
    }
}
