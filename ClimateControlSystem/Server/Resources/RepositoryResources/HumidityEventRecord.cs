using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public class HumidityEventRecord
    {
        [Key]
        public int Id { get; set; }
        public float Value { get; set; }
        public string Message { get; set; }
    }
}
