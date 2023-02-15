namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class MeasuredData
    {
        public float? MeasuredTemperature { get; set; }
        public float? MeasuredHumidity { get; set; }

        public MeasuredData Clone()
        {
            var clone = new MeasuredData()
            {
                MeasuredTemperature = MeasuredTemperature,
                MeasuredHumidity = MeasuredHumidity
            };

            return clone;
        }
    }
}
