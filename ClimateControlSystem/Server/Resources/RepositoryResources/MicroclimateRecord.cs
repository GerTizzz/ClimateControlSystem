using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class MicroclimateRecord
    {
        [Key]
        public int Id { get; set; }
        public int PredictionId { get; set; }
        public PredictionRecord Prediction { get; set; }
        public int SensorDataId { get; set; }
        public SensorsDataRecord SensorData { get; set; }
        public int? AccuracyId { get; set; }
        public AccuracyRecord? Accuracy { get; set; }
        public int? TemperatureEventId { get; set; }
        public TemperatureEventRecord? TemperatureEvent { get; set; }
        public int? HumidityEventId { get; set; }
        public HumidityEventRecord? HumidityEvent { get; set; }
    }
}
