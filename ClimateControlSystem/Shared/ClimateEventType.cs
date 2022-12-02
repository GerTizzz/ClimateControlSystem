namespace ClimateControlSystem.Shared
{
    public enum ClimateEventType : int
    {
        Normal = 0,
        PredictedTemperatureWarning = 1,
        PredictedHumidityWarning = 2,
        RealTemperatureCritical = 3,
        RealHumidityCritical = 4
    }
}
