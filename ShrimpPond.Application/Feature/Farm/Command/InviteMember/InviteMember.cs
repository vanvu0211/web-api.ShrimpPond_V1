using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Farm.Command.InviteMember
{
    public class InviteMember: IRequest<int>
    {
        public string Email { get; set; } = string.Empty;
        public int FarmId {  get; set; }
    }
}
