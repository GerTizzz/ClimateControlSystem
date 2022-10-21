using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources
{
    public class ClimateRecord
    {
        [Key]
        public int Id { get; set; }
        public long ArrivedTimeTicks { get; set; }
        public float ClusterLoad { get; set; }
        public float CpuUsage { get; set; }
        public float ClusterTemperature { get; set; }
        public float PreviousTemperature { get; set; }
        public float PreviousHumidity { get; set; }
        public float AirHumidityOutside { get; set; }
        public float AirDryTemperatureOutside { get; set; }
        public float AirWetTemperatureOutside { get; set; }
        public float WindSpeed { get; set; }
        public float WindDirection { get; set; }
        public float WindEnthalpy { get; set; }
        public float MeanCoolingValue { get; set; }
        public float PredictedTemperature { get; set; }
        public float PredictedHumidity { get; set; }
    }
}
