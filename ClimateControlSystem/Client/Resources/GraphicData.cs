namespace ClimateControlSystem.Client.Resources
{
    public record GraphicData
    {
        public string time { get; }

        public float value { get; }

        public string type { get; }

        public GraphicData(string x, float y, string fieldType)
        {
            time = x;
            value = y;
            type = fieldType;
        }

        public override string ToString()
        {
            return $"X: {time} Y: {value} Type: {type}";
        }
    }
}
