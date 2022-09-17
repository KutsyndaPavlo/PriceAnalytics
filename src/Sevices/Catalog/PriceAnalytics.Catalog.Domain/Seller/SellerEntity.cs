using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Proposal;
using PriceAnalytics.Catalog.Domain.Proposal.Rule;
using PriceAnalytics.Catalog.Domain.Seller.Events;

namespace PriceAnalytics.Catalog.Domain.Seller
{
    public class SellerEntity : BaseEntity, IAggregateRoot
    {
        private SellerEntity(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.IsActive = true;

            this._domainEvents.Add(new SellerCreatedEvent(this));
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public bool IsActive { get; private set; }

        public static SellerEntity CreateNewAndActivate(string name,
                                                        string description)
        {
            return new SellerEntity(name, description);
        }

        public bool CanBeDeleted()
        {
            return true;
        }

        public void UpdateSeller(
            string name,
            string? description)
        {
            this.Name = name;
            this.Description = description;
            this.LastModified = DateTime.UtcNow;

            this._domainEvents.Add(new SellerUpdatedEvent(this));
        }

        public void DeActivate()
        {
            IsActive = false;
            this._domainEvents.Add(new SellerDeactivatedEvent(this));
        }
    }
}
