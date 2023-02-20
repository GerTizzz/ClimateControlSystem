namespace ClimateControlSystem.Shared.Responses
{
    public sealed class PredictionsDTO
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public PredictionsDTO Clone()
        {
            return new PredictionsDTO
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
