using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.CreateFarm
{
    public class CreateFarm: IRequest<String>
    {
        public string FarmName {  get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string UserName {  get; set; } = string.Empty;
    }
}
