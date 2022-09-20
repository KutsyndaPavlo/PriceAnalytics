using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PriceAnalytics.Search.IntegrationEvents;
using PriceAnalytics.Search.Repository;

namespace PriceAnalytics.Search.Controllers
{
    [Produces("application/json")]
    [Route("api/proposal")]
    public class ProposalController : ControllerBase
    {
        private readonly IRepository<Item> _respository;

        public ProposalController(IRepository<Item> respository)
        {
            _respository = respository;
        }

        // GET: api/Items
        [HttpGet]
        [EnableQuery()]
        public async Task<IOrderedQueryable<Item>> Get()
        {
            return await _respository.GetItemsAsync();
        }
    }
}