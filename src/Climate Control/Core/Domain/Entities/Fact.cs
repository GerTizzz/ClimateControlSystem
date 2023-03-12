using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Fact : Entity
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Fact()
        {
            Id = Guid.NewGuid();
        }
    }
}
