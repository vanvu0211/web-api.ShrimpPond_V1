using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Farm
{
    public class FarmRole
    {
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
