namespace ClimateControlSystem.Client.Resources
{
    public record GraphicData
    {
        public string time { get; }

        public float value { get; }

        public string type { get; }

        public GraphicData(string x, float y, string dataType)
        {
            time = x;
            value = y;
            type = dataType;
        }

        public override string ToString()
        {
            return $"X: {time} Y: {value} Type: {type}";
        }
    }
}
