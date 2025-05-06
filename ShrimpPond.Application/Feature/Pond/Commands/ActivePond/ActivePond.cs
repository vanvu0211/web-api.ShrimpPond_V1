using MediatR;
using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.NurseryPond.Commands.ActiveNurseryPond
{
    public class ActivePond: IRequest<string>
    {
        public string pondId { get; set; } = string.Empty;
        public string seedId { get; set; } = string.Empty;
        public string seedName { get; set; } = string.Empty;

        public string? originPondId { get; set; } = string.Empty;
        public List<string>? certificates { get; set; }
        public float sizeShrimp { get; set; }
        public float amountShrimp { get; set; }
    }
}
