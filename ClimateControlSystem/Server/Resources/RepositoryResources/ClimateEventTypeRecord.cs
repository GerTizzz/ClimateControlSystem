using ClimateControlSystem.Shared;
using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class ClimateEventTypeRecord
    {
        [Key]
        public int Id { get; set; }
        public ClimateEventType EventType { get; set; }
        public List<PredictionRecord> Predictions { get; set; }
    }
}
