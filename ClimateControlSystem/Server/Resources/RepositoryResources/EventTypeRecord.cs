using ClimateControlSystem.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class EventTypeRecord
    {
        [Key]
        public int Id { get; set; }
        public ClimateEventType EventType { get; set; }
        public List<ClimateRecord> Climates { get; set; } = new();
    }
}
