using System.ComponentModel.DataAnnotations;

namespace Domain.Primitives
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; protected set; }

        protected Entity(Guid id) => Id = id;
        
        protected Entity() { }
    }
}
