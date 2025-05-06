using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Models.Sms
{
    public class SmsMessage
    {
        public string FROM { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
}
