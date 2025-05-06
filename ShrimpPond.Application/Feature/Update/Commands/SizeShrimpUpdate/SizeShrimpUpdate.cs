using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.SizeShrimpUpdate
{
    public class SizeShrimpUpdate: IRequest<string>
    {
        public string pondId { get; set; } = string.Empty;
        public float sizeValue { get; set; }
        public DateTime updateDate { get; set; }

    }
}
