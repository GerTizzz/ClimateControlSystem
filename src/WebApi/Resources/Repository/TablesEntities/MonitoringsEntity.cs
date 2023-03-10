using System.ComponentModel.DataAnnotations;

namespace WebApi.Resources.Repository.TablesEntities
{
    public sealed class MonitoringsEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset? TracedTime { get; set; }
        public int? PredictionId { get; set; }
        public PredictionsEntity? Prediction { get; set; }
        public int? AccuracyId { get; set; }
        public AccuracysEntity? Accuracy { get; set; }
        public int? ActualDataId { get; set; }
        public ActualDataEntity? ActualData { get; set; }
        public int? MicroclimatesEventId { get; set; }
        public MicroclimatesEventsEntity? MicroclimatesEvent { get; set; }
    }
}
