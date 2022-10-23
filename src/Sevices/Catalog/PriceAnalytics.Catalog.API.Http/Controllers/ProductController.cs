using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.Catalog.API.Models;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct;
using PriceAnalytics.Catalog.Application.Product.Commands.DeactivateProduct;
using PriceAnalytics.Catalog.Application.Product.Commands.DeleteProduct;
using PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductProposals;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProducts;

namespace PriceAnalytics.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProductController(
            IMediator mediator,
            ILogger<ProductController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions

        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Product" }, Summary = "Get all products.")]
        //[Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(IEnumerable<ProductDto>))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetProductsQuery(name, offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific product  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a product.</param>
        /// <returns>
        /// A <see cref="ProductDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        // [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Get product by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))] 
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "RProduct does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets a specific product  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a product.</param>
        /// <returns>
        /// A <see cref="ProductDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}/proposals")]
        // [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Get product by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))] 
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "RProduct does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetProposals(Guid id)
        {
            var query = new GetProductProposalsQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        //[SwaggerOperation(Tags = new[] { "Product" }, Summary = "Create product.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Product already exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateProductCommand>(request));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        // [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Update product.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Product has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateProductRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<UpdateProductCommand>(request));
            return Ok(result);
        }

        [HttpPut("{id}/deactivate")]
        // [SwaggerOperation(Tags = new[] { "Product" }, Summary = "Update product.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProductDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Product has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeactivateAsync(Guid id)
        {
            var result = await _mediator.Send(new DeactivateProductCommand { Id = id });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(Tags = new[] { "Product" }, Summary = "Delete product by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status204NoContent)]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Product does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
