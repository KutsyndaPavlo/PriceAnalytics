namespace PriceAnalytics.Catalog.Domain.Common
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.Created = DateTime.UtcNow;
            this.LastModified = this.Created;
        }

        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        // public string? CreatedBy { get; set; }

        public DateTime LastModified { get; protected set; }

        //  public string? LastModifiedBy { get; set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected List<DomainEvent> _domainEvents { get; set; } = new List<DomainEvent>();

        public void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
