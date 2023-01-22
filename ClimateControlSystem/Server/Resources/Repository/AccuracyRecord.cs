using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class AccuracyRecord
    {
        [Key]
        public int Id { get; set; }
        public float PredictedTemperatureAccuracy { get; set; }
        public float PredictedHumidityAccuracy { get; set; }
    }
}
