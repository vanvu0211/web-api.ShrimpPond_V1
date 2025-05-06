using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.Environments
{
    public class EnvironmentStatus
    {
        public int EnvironmentStatusId {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTime? Timestamp   { get; set; }

        public string PondId { get; set; } = string.Empty;
        public Domain.PondData.Pond Pond { get; set; }

    }
}
