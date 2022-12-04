using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class ConfigRecord
    {
        [Key]
        public int Id { get; set; }
        public float TemperatureLimit { get; set; }
        public float HumidityLimit { get; set; }
    }
}
