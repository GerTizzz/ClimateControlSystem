using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Accuracy : Entity
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Accuracy(Guid id, float temperature, float humidity) : base(id)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
