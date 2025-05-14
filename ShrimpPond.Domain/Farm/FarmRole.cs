using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Farm
{
    public class FarmRole
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
        public int FarmId  { get; set; }
        public Farm Farm { get; set; }
    }
}
