namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class MicroclimateEvent
    {
        public float? TemperatureValue { get; set; }
        public float? HumidityValue { get; set; }

        public MicroclimateEvent Clone()
        {
            var clone = new MicroclimateEvent()
            {
                TemperatureValue = TemperatureValue,
                HumidityValue = HumidityValue
            };

            return clone;
        }
    }
}
