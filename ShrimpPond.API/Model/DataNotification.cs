namespace ShrimpPond.API.Model
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
