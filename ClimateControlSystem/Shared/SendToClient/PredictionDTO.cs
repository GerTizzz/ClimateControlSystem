namespace ClimateControlSystem.Shared.SendToClient
{
    public sealed class PredictionDTO
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public PredictionDTO Clone()
        {
            return new PredictionDTO
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
