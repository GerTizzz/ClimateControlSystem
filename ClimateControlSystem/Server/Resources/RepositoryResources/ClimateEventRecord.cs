using ClimateControlSystem.Server.Resources.Common;
using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.RepositoryResources
{
    public sealed class ClimateEventRecord
    {
        [Key]
        public int Id { get; set; }
        public ClimateEventType EventType { get; set; }
    }
}
