using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class MicroclimatesEventsEntity
    {
        [Key]
        public int Id { get; set; }
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }

        /// <summary>
        /// Instance clone without Id
        /// </summary>
        public MicroclimatesEventsEntity Clone()
        {
            return new MicroclimatesEventsEntity
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
