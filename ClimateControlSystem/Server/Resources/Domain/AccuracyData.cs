namespace ClimateControlSystem.Server.Resources.Domain
{
    public sealed class Accuracy
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Accuracy Clone()
        {
            var clone = new Accuracy()
            {
                Temperature = Temperature,
                Humidity = Humidity
            };

            return clone;
        }
    }
}
