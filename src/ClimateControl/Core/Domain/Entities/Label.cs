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

        public Label Clone()
        {
            return new Label(Id)
            {
                Temperature = Temperature,
                Humidity = Humidity
            };
        }
    }
}
