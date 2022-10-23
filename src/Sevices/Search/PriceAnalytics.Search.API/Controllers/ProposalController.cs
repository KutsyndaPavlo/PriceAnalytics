using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PriceAnalytics.Search.Model;
using PriceAnalytics.Search.Repository;

namespace PriceAnalytics.Search.Controllers
{
    [Produces("application/json")]
    [Route("api/proposal")]
    public class ProposalController : ControllerBase
    {
        private readonly IRepository<ProductProposal> _respository;

        public ProposalController(IRepository<ProductProposal> respository)
        {
            _respository = respository;
        }

        // GET: api/Items
        [HttpGet]
        [EnableQuery()]
        public async Task<IOrderedQueryable<ProductProposal>> Get()
        {
            return await _respository.GetItemsAsync();
        }
    }
}