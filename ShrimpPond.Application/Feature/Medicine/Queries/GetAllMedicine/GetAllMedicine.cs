using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Queries.GetAllMedicine
{
    public class GetAllMedicine: IRequest<List<GetAllMedicineDTO>>
    {
        public int farmId { get; set; } 
        public GetAllMedicine(int farmId)
        {
            this.farmId = farmId;
        }
    }
}
