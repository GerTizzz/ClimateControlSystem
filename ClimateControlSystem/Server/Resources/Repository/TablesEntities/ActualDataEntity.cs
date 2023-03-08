using System.ComponentModel.DataAnnotations;

namespace ClimateControl.Server.Resources.Repository.TablesEntities
{
    public class ActualDataEntity
    {
        [Key]
        public int Id { get; set; }

        public float Temperature { get; set; }
        public float Humidity { get; set; }

        /// <summary>
        /// Instance clone without Id
        /// </summary>
        public ActualDataEntity Clone()
        {
            return new ActualDataEntity
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
