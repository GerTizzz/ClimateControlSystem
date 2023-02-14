using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class MicroclimatesEventsEntity
    {
        [Key]
        public int Id { get; set; }
        public float? TempertatureValue { get; set; }
        public float? HumidityValue { get; set; }
    }
}
