using System.ComponentModel.DataAnnotations;

namespace ClimateControlSystem.Server.Resources.Repository.TablesEntities
{
    public sealed class PredictionsEntity
    {
        [Key]
        public int Id { get; set; }
        public float PredictedTemperature { get; set; }
        public float PredictedHumidity { get; set; }
    }
}
