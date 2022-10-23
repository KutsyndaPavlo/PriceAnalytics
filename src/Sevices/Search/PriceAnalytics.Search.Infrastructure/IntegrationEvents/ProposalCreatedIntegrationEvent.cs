using PriceAnalytics.Infrustructure.EventBus.Events;

namespace PriceAnalytics.Search.IntegrationEvents
{
    public record ProposalCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid ProductId { get; set; }

        public Guid SellerId { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string SellerName { get; set; }
    }
}
