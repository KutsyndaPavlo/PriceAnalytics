using PriceAnalytics.Infrustructure.EventBus.Abstractions;
using PriceAnalytics.Search.Repository;

namespace PriceAnalytics.Search.IntegrationEvents.EventHandling
{
    public class ProposalCreatedIntegrationEventHandler : IIntegrationEventHandler<ProposalCreatedIntegrationEvent>
    {

        private readonly IRepository<ProposalCreatedIntegrationEvent> _respository;

        public ProposalCreatedIntegrationEventHandler(IRepository<ProposalCreatedIntegrationEvent> respository)
        {
            _respository = respository;
        }

        public async Task Handle(ProposalCreatedIntegrationEvent @event)
        {
            await _respository.CreateItemAsync(@event);
        }
    }

}
