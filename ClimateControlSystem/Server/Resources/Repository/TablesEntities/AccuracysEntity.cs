using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class AccuracysEntity
    {
        [Key]
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        /// <summary>
        /// Instnace clone without Id
        /// </summary>
        public AccuracysEntity Clone()
        {
            return new AccuracysEntity
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
