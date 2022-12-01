using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class ConfigRecord
    {
        [Key]
        public int Id { get; set; }
        public double TemperatureLimit { get; set; }
        public double HumidityLimit { get; set; }
    }
}
