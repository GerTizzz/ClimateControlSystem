namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class ActualDataResponse
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public ActualDataResponse Clone()
        {
            return new ActualDataResponse
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
