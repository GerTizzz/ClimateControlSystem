namespace ClimateControlSystem.Server.Resources.Common
{
    public sealed class MonitoringWithEvents : BaseMonitoring
    {
        /// <remarks> Can be null in case if prediction value is in allowed interval </remarks>
        public TemperatureEvent? TemperaturePredictionEvent { get; set; }
        /// <remarks> Can be null in case if prediction value is in allowed interval </remarks>
        public HumidityEvent? HumidityPredictionEvent { get; set; }
    }
}
