using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class MonitoringRecord
    {
        [Key]
        public int Id { get; set; }
        public int? PredictionId { get; set; }
        public PredictionRecord? Prediction { get; set; }
        public int? SensorDataId { get; set; }
        public SensorsDataRecord? SensorData { get; set; }
        public int? AccuracyId { get; set; }
        public AccuracyRecord? Accuracy { get; set; }
        public int? MicroclimateEventId { get; set; }
        public MicroclimateEventRecord? MicroclimateEvent { get; set; }
    }
}
