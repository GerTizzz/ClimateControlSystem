using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class MicroclimatesEventsEntity
    {
        [Key]
        public int Id { get; set; }
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }
    }
}
