using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Error : Entity
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Error(Guid id, float temperature, float humidity) : base(id)
        {
            Temperature = temperature;
            Humidity = humidity;
        }

        public Error Clone()
        {
            return new Error(Id, Temperature, Humidity);
        }
    }
}
