namespace ClimateControlSystem.Client.Resources
{
    public record GraphicData
    {
        public string time { get; }

        public float value { get; }

        public string type { get; }

        public GraphicData(string time, float value, string type)
        {
            this.time = time;
            this.value = value;
            this.type = type;
        }
    }
}
