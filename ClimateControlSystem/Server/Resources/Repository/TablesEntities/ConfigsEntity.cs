using System.ComponentModel.DataAnnotations;

namespace ClimateControl.Server.Resources.Repository.TablesEntities
{
    public sealed class ConfigsEntity
    {
        [Key]
        public int Id { get; set; }
        public float UpperTemperatureWarningLimit { get; set; }
        public float LowerTemperatureWarningLimit { get; set; }

        public float UpperHumidityWarningLimit { get; set; }
        public float LowerHumidityWarningLimit { get; set; }

        public int PredictionTimeIntervalSeconds { get; set; }
    }
}
