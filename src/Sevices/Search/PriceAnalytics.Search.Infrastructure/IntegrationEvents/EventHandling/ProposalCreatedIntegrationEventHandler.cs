using PriceAnalytics.Infrustructure.EventBus.Abstractions;
using PriceAnalytics.Search.Model;
using PriceAnalytics.Search.Repository;

namespace PriceAnalytics.Search.IntegrationEvents.EventHandling
{
    public class ProposalCreatedIntegrationEventHandler : IIntegrationEventHandler<ProposalCreatedIntegrationEvent>
    {

        private readonly IRepository<ProductProposal> _respository;

        public ProposalCreatedIntegrationEventHandler(IRepository<ProductProposal> respository)
        {
            _respository = respository;
        }

        public async Task Handle(ProposalCreatedIntegrationEvent @event)
        {
            var item = new ProductProposal()
            {
                Currency = @event.Currency,
                ProductName = @event.ProductName,
                Date = @event.Date,
                Description = @event.Description,
                Price = @event.Price,
                ProductCode = @event.ProductCode,
                ProductId = @event.ProductId,
                SellerId = @event.SellerId,
                SellerName = @event.SellerName,
                Id = @event.Id,
            };

            await _respository.AddItemAsync(item);
        }
    }

}
