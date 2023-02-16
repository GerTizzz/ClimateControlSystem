namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class PredictionResponse
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public PredictionResponse Clone()
        {
            return new PredictionResponse
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
