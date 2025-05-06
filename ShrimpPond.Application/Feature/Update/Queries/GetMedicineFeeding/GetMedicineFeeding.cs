using MediatR;
using ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetMedicineFeeding
{
    public class GetMedicineFeeding : IRequest<List<GetMedicineFeedingDTO>>
    {
        public string pondId { get; set; } = string.Empty;

    }

}
