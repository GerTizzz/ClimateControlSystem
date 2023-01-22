using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class TemperatureEventRecord
    {
        [Key]
        public int Id { get; set; }
        public float Value { get; set; }
    }
}
