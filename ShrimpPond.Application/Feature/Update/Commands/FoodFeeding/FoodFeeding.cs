using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShrimpPond.Domain.PondData;

namespace ShrimpPond.Application.Feature.Feeding.Commands.Feeding
{
    public class FoodFeeding : IRequest<string>
    {
        public string pondId { get; set; } = string.Empty;
        public List<Foods>? foods { get; set; }
        public DateTime feedingDate { get; set; }

    }
}
