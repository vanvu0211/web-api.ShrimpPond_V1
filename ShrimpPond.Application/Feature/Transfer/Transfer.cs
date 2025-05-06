using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Transfer
{
    public class Transfer: IRequest<string>
    {
        public string transferPondId { get; set; } = string.Empty;
        public string originPondId { get; set; } = string.Empty;
        public DateTime transferDate { get; set; }
        public float size { get; set; }
        public float amount { get; set; }

    }
}
