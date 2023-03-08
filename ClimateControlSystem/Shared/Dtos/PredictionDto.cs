namespace ClimateControl.Shared.Dtos
{
    public sealed class PredictionDto
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public PredictionDto Clone()
        {
            return new PredictionDto
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
