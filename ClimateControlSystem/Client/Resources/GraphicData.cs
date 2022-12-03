namespace ClimateControlSystem.Client.Resources
{
    public record GraphicData
    {
        public string X { get; }

        public float Y { get; }

        public string Type { get; }

        public GraphicData(string x, float y, string type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Type: {Type}";
        }
    }
}
