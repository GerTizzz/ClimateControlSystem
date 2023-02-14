using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class AccuracysEntity
    {
        [Key]
        public int Id { get; set; }
        public float PredictedTemperatureAccuracy { get; set; }
        public float PredictedHumidityAccuracy { get; set; }
    }
}
