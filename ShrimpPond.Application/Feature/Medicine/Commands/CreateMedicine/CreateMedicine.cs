using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.CreateMedicine
{
    public class CreateMedicine: IRequest<string>   
    {
        public string name { get; set; } = string.Empty;
        public int farmId{ get; set; }
    }
}
