namespace ShrimpPond.Host.MQTTModels
{
    public class EnviromentSend
    {
        public string PondId { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string value { get; set; } = string.Empty;
        public string timestamp { get; set; } = string.Empty;
    }
}
