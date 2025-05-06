using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Host.Model
{
    public class DataNotification
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow.AddHours(7);

        public DataNotification(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
