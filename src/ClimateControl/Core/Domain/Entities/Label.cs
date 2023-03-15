using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Label : Entity
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public Label(Guid id) : base(id)
        {

        }
    }
}
