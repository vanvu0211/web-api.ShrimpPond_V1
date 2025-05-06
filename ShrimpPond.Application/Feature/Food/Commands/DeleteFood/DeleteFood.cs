using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Food.Commands.DeleteFood
{
    public class DeleteFood: IRequest<string>
    {
        public int foodId { get; set; } 
    }
}
