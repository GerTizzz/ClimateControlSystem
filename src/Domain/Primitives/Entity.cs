namespace Domain.Primitives
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid Id) => Id = Id;

        public Entity()
        {
            
        }
    }
}
