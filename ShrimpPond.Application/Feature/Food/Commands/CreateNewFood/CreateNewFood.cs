using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.CreateNewFood
{
    public class CreateNewFood: IRequest<string>
    {
        public int farmId { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
