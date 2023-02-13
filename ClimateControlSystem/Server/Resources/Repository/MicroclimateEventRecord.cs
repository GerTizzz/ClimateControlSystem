using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class MicroclimateEventRecord
    {
        [Key]
        public int Id { get; set; }
        public float? TempertatureValue { get; set; }
        public float? HumidityValue { get; set; }
    }
}
