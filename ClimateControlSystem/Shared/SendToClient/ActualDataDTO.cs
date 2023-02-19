namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class ActualDataDTO
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public ActualDataDTO Clone()
        {
            return new ActualDataDTO
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
