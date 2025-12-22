namespace Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected void MarkAsModified()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
