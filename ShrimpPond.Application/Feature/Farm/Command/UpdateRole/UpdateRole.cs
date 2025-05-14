using MediatR;
using ShrimpPond.Domain.Farm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.UpdateRole
{
    public class UpdateRole: IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
        public string UpdateEmail { get; set; } = string.Empty;
        public Role Role { get; set; }
        public int FarmId { get; set; }
    }
}
