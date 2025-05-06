using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Infrastructure.Communication
{
    public class MqttMessage
    {
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public MqttMessage(string topic, string payload)
        {
            Topic = topic;
            Payload = payload;
        }
    }
}
