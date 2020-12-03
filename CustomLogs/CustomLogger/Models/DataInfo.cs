namespace CustomLogs.Models
{
    public class DataInfo
    {
        public DataInfo(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
