using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.ApiGateway.Models;
using PriceAnalytics.ApiGateway.Product;
using System.Net;

namespace PriceAnalytics.ApiGateway.Controllers
{
    [Route("api/v1/catalog/product")]
    [ApiController]
    public class ProductController : Controller
    {
        #region Fields

        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly string _catalogApiUrl = Environment.GetEnvironmentVariable("PA_CATALOG_URL");

        #endregion

        #region Constructor

        public ProductController(ILogger<ProductController> logger, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        #endregion

        #region Actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">
        /// <p style="font-size:13px">The following validation rules apply to the name:</p>
        /// <ul style="font-size:13px">
        /// <li>example 1</li>
        /// <li>example 2</li>
        /// </ul>
        /// </param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Models.PaginatedList<Models.ProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var result = await client.GetAsync(new GetProductsQuery
            {
                Name = name ?? "",
                Limit = limit,
                Offset = offset
            });

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.PaginatedList<Models.ProductDto>>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        /// <summary>
        /// Gets a specific product  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a product.</param>
        /// <returns>
        /// A <see cref="Models.ProductDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Models.ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var result = await client.GetByIdAsync(new GetProductByIdQuery
            {
                Id = id.ToString()
            });

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.ProductDto>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        ///// <summary>
        ///// Gets a specific product  by the supplied definition Id.
        ///// </summary>
        ///// <param name="id">System generated ID returned when create a product.</param>
        ///// <returns>
        ///// A <see cref="ProductDto" /> which matches the input id.
        ///// </returns>
        //[HttpGet("{id}/proposals")]
        //// [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Get product by id.")]
        //[Produces("application/json")]
        ////[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))] 
        ////[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        ////[SwaggerResponse(StatusCodes.Status404NotFound, "RProduct does not exist")]
        ////[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        //public async Task<IActionResult> GetProposals(Guid id)
        //{
        //    var query = new GetProductProposalsQuery(id);
        //    var result = await _mediator.Send(query);

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Models.ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var result = await client.CreateAsync(_mapper.Map<CreateProductCommand>(request));

            if (result.Code == StatusCodes.Status201Created)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, _mapper.Map<Models.ProductDto>(result.Data));
            }

            return new JsonResult(result.Message)
            {
                StatusCode = result.Code
            };
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Models.ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateProductRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var result = await client.UpdateAsync(_mapper.Map<UpdateProductCommand>(request));

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.ProductDto>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType(typeof(Models.ProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [Produces("application/json")]
        public async Task<IActionResult> PatchAsync(Guid id, [FromBody] DeactivateProductRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var result = await client.DeactivateAsync(_mapper.Map<DeactivateProductCommand>(request));

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.ProductDto>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(Guid id)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var result = await client.DeleteAsync(new DeleteProductCommand { Id = id.ToString() });

            return new JsonResult(result.Message)
            {
                StatusCode = result.Code                
            };
        }

        #endregion
    }
}
