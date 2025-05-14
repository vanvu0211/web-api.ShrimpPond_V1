using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Farm
{
    public class FarmRole
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }

        public int FarmId  { get; set; }
        public Farm Farm { get; set; }
    }
}
