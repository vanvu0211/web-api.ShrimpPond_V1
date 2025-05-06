using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.CreatePond
{
    public class CreatePond:IRequest<string>
    {
        public string pondId { get; set; } = string.Empty;
        public string pondName { get; set; } = string.Empty;
        public string pondTypeId { get; set; } = string.Empty ;
        public float deep { get; set; }
        public float diameter { get; set; }
    }
}
