using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Proposal;
using PriceAnalytics.Catalog.Domain.Proposal.Events;
using PriceAnalytics.Catalog.Domain.Proposal.Rule;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Domain.Entities
{
    public class ProposalEntity : BaseEntity, IAggregateRoot
    {
        private ProposalEntity(Guid productId, Guid sellerId, float price, string currency, string description)
        {
            ProductId = productId;
            SellerId = sellerId;
            Description = description;
            IsActive = true;
            Date = DateTime.UtcNow.Date;
            Price = new MoneyValue(price, currency);
        }

        private ProposalEntity() { }

        public Guid ProductId { get; private set; }

        public Guid SellerId { get; private set; }

        public MoneyValue Price { get; private set; }

        public string? Description { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsActive { get; private set; }

        public static ProposalEntity CreateNewAndActivate(
            ProductEntity product, 
            SellerEntity seller,
            float price,
            string currency, 
            string description,
            IProposalService proposalService)
        {
            CheckRule(new OnlyActiveSellerCanProvidePoposalRule(seller));
            CheckRule(new ProposalShouldBeProvidedToActiveProductRule(product));
            CheckRule(new ProposalPriceMustBeGreaterThanZeroRule(price));
            CheckRule(new CurrencyOfProposalPriceMustBeUahOrUsd(currency));
           
            var entity = new ProposalEntity(product.Id, seller.Id, price, currency, description);

            CheckRule(new ShouldBeOnlyOneProposalPerDayRule(proposalService, entity));

            entity.AddDomainEvent(new ProposalCreatedEvent(entity, product, seller));

            return entity;
        }

        public void DeActivate()
        {
            IsActive = false;
            this._domainEvents.Add(new ProposalDeactivatedEvent(this));
        }
    }
}
