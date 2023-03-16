using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Fact : Entity
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Fact(Guid id) : base(id)
        {

        }

        public Fact Clone()
        {
            return new Fact(Id)
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
