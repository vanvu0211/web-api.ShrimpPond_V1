using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.UpdateMedicine
{
    public class UpdateMedicine : IRequest<string>
    {
        public int medicineId { get; set; }
        public string newName { get; set; } = string.Empty;
    }
}
