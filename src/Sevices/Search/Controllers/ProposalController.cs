using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.Search.IntegrationEvents;
using PriceAnalytics.Search.Repository;
using System.Web.Http.OData;

namespace PriceAnalytics.Search.Controllers
{
    [Produces("application/json")]
    [Route("api/proposal")]
    public class ProposalController : ControllerBase
    {
        private readonly IRepository<ProposalCreatedIntegrationEvent> _respository;

        public ProposalController(IRepository<ProposalCreatedIntegrationEvent> respository)
        {
            _respository = respository;
        }

        // GET: api/Items
        [HttpGet]
        [EnableQuery()]
        public async Task<IEnumerable<ProposalCreatedIntegrationEvent>> Get()
        {
            return await _respository.GetItemsAsync(x => x != null);
        }
    }
}