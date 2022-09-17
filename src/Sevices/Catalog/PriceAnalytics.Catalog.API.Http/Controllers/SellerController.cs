using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.Catalog.API.Models;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller;
using PriceAnalytics.Catalog.Application.Seller.Commands.DeactivateSeller;
using PriceAnalytics.Catalog.Application.Seller.Commands.DeleteSeller;
using PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellers;

namespace PriceAnalytics.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<SellerController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public SellerController(
            IMediator mediator,
            ILogger<SellerController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions

        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Seller" }, Summary = "Get all Sellers.")]
        //[Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(IEnumerable<SellerDto>))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetSellersQuery(offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific Seller  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a Seller.</param>
        /// <returns>
        /// A <see cref="SellerDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        // [SwaggerOperation(Tags = new[] { "Seller" }, Summary = "Get Seller by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(SellerDto))] 
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "RSeller does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetSellerByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        //[SwaggerOperation(Tags = new[] { "Seller" }, Summary = "Create Seller.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(SellerDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Seller already exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateSellerRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateSellerCommand>(request));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        // [SwaggerOperation(Tags = new[] { "Seller" }, Summary = "Update Seller.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(SellerDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Seller does not exist")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Seller has been updated by someone else")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateSellerRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<UpdateSellerCommand>(request));
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
            var result = await _mediator.Send(new DeactivateSellerCommand { Id = id });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(Tags = new[] { "Seller" }, Summary = "Delete Seller by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status204NoContent)]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Seller does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteSellerCommand { Id = id };

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
