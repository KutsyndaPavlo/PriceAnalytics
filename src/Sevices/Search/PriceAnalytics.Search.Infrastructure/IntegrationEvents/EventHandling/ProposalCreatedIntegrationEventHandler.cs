using PriceAnalytics.Infrustructure.EventBus.Abstractions;
using PriceAnalytics.Search.Model;
using PriceAnalytics.Search.Repository;

namespace PriceAnalytics.Search.IntegrationEvents.EventHandling
{
    public class ProposalCreatedIntegrationEventHandler : IIntegrationEventHandler<ProposalCreatedIntegrationEvent>
    {

        private readonly IRepository<ProductProposal> _respository;

        public ProposalCreatedIntegrationEventHandler()
        //public ProposalCreatedIntegrationEventHandler(IRepository<ProductProposal> respository)
        {
            //_respository = respository;
        }

        public async Task Handle(ProposalCreatedIntegrationEvent @event)
        {
            //await _respository.CreateItemAsync(@event);
        }
    }

}
