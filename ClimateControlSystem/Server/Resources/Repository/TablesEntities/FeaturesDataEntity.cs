using System.ComponentModel.DataAnnotations;

namespace ClimateControl.Server.Resources.Repository.TablesEntities
{
    public sealed class FeaturesDataEntity
    {
        [Key]
        public int Id { get; set; }

        public float ClusterLoad { get; set; }
        public float CpuUsage { get; set; }
        public float ClusterTemperature { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float AirHumidityOutside { get; set; }
        public float AirDryTemperatureOutside { get; set; }
        public float AirWetTemperatureOutside { get; set; }
        public float WindSpeed { get; set; }
        public float WindDirection { get; set; }
        public float WindEnthalpy { get; set; }
        public float CoolingValue { get; set; }
    }
}
