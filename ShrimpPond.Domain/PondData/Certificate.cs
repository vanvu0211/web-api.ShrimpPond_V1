using ShrimpPond.Domain.PondData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Domain.PondData
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string CertificateName { get; set; } = string.Empty;
        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[]? FileData { get; set; }

        public string PondId { get; set; } = string.Empty;
        public Pond? Pond { get; set; }

    }
}
