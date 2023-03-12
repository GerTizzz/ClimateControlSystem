using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Warning : Entity
    {
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }

        public Warning(Guid id) : base(id)
        {

        }
    }
}
