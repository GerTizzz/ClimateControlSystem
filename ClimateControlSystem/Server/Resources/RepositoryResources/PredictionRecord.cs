using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class PredictionRecord
    {
        [Key]
        public int Id { get; set; }
        public float PredictedTemperature { get; set; }
        public float PredictedHumidity { get; set; }
    }
}
