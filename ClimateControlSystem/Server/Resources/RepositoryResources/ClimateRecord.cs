using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class ClimateRecord
    {
        [Key]
        public int Id { get; set; }
        public int PredictionId { get; set; }
        public PredictionRecord Prediction { get; set; }
        public int ConfigId { get; set; }
        public ConfigRecord Config { get; set; }
        public int MonitoringDataId { get; set; }
        public MonitoringRecord MonitoringData { get; set; }
        public int? AccuracyId { get; set; }
        public AccuracyRecord? Accuracy { get; set; }
        public List<EventTypeRecord> Events { get; set; } = new();
    }
}
