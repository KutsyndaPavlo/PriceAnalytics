using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.ApiGateway.Models;
using PriceAnalytics.ApiGateway.Product;
using System.Net;

namespace PriceAnalytics.ApiGateway.Controllers
{
    [Route("api/v1/search")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly string _searchApiUrl = Environment.GetEnvironmentVariable("PA_SEARCH_URL");
        private readonly HttpClient _httpClient;

        public SearchController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpGet]
        //[ProducesResponseType(typeof(Models.PaginatedList<Models.ProductDto>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            _httpClient.BaseAddress = new Uri(_searchApiUrl);

            var result = await _httpClient.GetAsync($"api/proposal/{this.HttpContext.Request.QueryString}");
            var body = await result.Content.ReadAsStringAsync();

            return new JsonResult(body)
            {
                StatusCode = (int)result.StatusCode
            };
        }
    }
}
