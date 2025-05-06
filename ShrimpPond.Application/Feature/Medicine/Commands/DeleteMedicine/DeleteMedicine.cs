using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Medicine.Commands.DeleteMedicine
{
    public class DeleteMedicine : IRequest<string>
    {
        public int medicineId { get; set; }
    }
    }
