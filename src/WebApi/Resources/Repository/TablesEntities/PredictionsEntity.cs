using System.ComponentModel.DataAnnotations;

namespace WebApi.Resources.Repository.TablesEntities
{
    public sealed class PredictionsEntity
    {
        [Key]
        public int Id { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int FeaturesId { get; set; }
        public FeaturesDataEntity Features { get; set; }
    }
}
