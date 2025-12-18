



namespace Shared.DDD
{
    public abstract class Aggregate<TId> : BaseEntity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
