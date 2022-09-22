using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Domain.Proposal.Events
{
    public class ProposalCreatedEvent : DomainEvent
    {
        public ProposalCreatedEvent(ProposalEntity proposal, ProductEntity product, SellerEntity seller)
        {
            Proposal = proposal;
            Product = product;
            Seller = seller;
        }

        public ProposalEntity Proposal { get; }

        public ProductEntity Product { get; }

        public SellerEntity Seller { get; }
    }
}