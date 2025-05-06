using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding
{
    public class GetFoodFeeding: IRequest<List<GetFoodFeedingDTO>>
    {
        public string pondId { get; set; } = string.Empty;
    }
}
