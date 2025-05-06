using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.UpdateFood
{
    public class UpdateFood : IRequest<string>
    {
        public int foodId { get; set; }
        public string newName { get; set; } = string.Empty;

    }
}
