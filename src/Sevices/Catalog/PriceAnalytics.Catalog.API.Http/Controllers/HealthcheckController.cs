using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PriceAnalytics.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Healthcheck" }, Summary = "Get service status.")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            return Ok(true);
        }
    }
}
