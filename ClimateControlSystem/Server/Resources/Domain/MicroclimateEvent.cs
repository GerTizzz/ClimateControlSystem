namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class MicroclimatesEvents
    {
        public float? TemperatureValue { get; set; }
        public float? HumidityValue { get; set; }

        public MicroclimatesEvents Clone()
        {
            var clone = new MicroclimatesEvents()
            {
                TemperatureValue = TemperatureValue,
                HumidityValue = HumidityValue
            };

            return clone;
        }
    }
}
