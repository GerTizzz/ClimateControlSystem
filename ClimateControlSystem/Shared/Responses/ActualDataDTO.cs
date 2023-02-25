namespace ClimateControlSystem.Shared.Responses
{
    public sealed class ActualDataDto
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public ActualDataDto Clone()
        {
            return new ActualDataDto
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
