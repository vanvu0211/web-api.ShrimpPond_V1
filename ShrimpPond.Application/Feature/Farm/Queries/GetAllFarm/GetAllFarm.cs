using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Queries.GetAllFarm
{
    public class GetAllFarm: IRequest<List<FarmDTO>>
    {
        public string userName { get; set; } = string.Empty;
    }
}
