using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Commands.LossShrimpUpdate
{
    public class LossShrimpUpdate: IRequest<string>
    {
        public string pondId { get; set; } = string.Empty;
        public float lossValue { get; set; }
        public DateTime updateDate { get; set; }

    }
}
