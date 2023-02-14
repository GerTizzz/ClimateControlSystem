using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class MonitoringsEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset? MeasurementTime { get; set; }
        public int? PredictionsId { get; set; }
        public PredictionsEntity? Prediction { get; set; }
        public int? SensorsDataId { get; set; }
        public SensorsDataEntity? SensorsData { get; set; }
        public int? AccuracysId { get; set; }
        public AccuracysEntity? Accuracy { get; set; }
        public int? MicroclimatesEventsId { get; set; }
        public MicroclimatesEventsEntity? MicroclimatesEvent { get; set; }
    }
}
