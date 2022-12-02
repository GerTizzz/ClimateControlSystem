using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class PredictionRecord
    {
        [Key]
        public int Id { get; set; }
        public float PredictedTemperature { get; set; }
        public float PredictedHumidity { get; set; }
        public int MonitoringDataId { get; set; }
        public MonitoringRecord MonitoringData { get; set; }
        public int? AccuracyId { get; set; }
        public AccuracyRecord? Accuracy { get; set; }
        public List<ClimateEventTypeRecord> ClimateEvents { get; set; }
    }
}
