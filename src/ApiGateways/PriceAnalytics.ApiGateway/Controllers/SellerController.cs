using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.ApiGateway.Models;
using PriceAnalytics.ApiGateway.Seller;
using System.Net;

namespace PriceAnalytics.ApiGateway.Controllers
{
    [Route("api/v1/catalog/seller")]
    [ApiController]
    public class SellerController : Controller
    {
        #region Fields

        private readonly ILogger<SellerController> _logger;
        private readonly IMapper _mapper;
        private readonly string _catalogApiUrl = Environment.GetEnvironmentVariable("PA_CATALOG_URL");

        #endregion

        #region Constructor

        public SellerController(ILogger<SellerController> logger, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        #endregion

        #region Actions

        [HttpGet]
        [ProducesResponseType(typeof(Models.PaginatedList<Models.SellerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new SellerServiceProto.SellerServiceProtoClient(channel);
            var result = await client.GetAsync(new GetSellersQuery
            {
                Name = name ?? "",
                Limit = limit,
                Offset = offset
            });

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.PaginatedList<Models.SellerDto>>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        /// <summary>
        /// Gets a specific Seller  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a Seller.</param>
        /// <returns>
        /// A <see cref="Models.SellerDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Models.SellerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new SellerServiceProto.SellerServiceProtoClient(channel);
            var result = await client.GetByIdAsync(new GetSellerByIdQuery
            {
                Id = id.ToString()
            });

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.SellerDto>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        ///// <summary>
        ///// Gets a specific Seller  by the supplied definition Id.
        ///// </summary>
        ///// <param name="id">System generated ID returned when create a Seller.</param>
        ///// <returns>
        ///// A <see cref="SellerDto" /> which matches the input id.
        ///// </returns>
        //[HttpGet("{id}/proposals")]
        //// [SwaggerOperation(Tags = new[] { "Seller" }, Summary = "Get Seller by id.")]
        //[Produces("application/json")]
        ////[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(SellerDto))] 
        ////[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        ////[SwaggerResponse(StatusCodes.Status404NotFound, "RSeller does not exist")]
        ////[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        //public async Task<IActionResult> GetProposals(Guid id)
        //{
        //    var query = new GetSellerProposalsQuery(id);
        //    var result = await _mediator.Send(query);

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Models.SellerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] CreateSellerRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new SellerServiceProto.SellerServiceProtoClient(channel);
            var result = await client.CreateAsync(_mapper.Map<CreateSellerCommand>(request));

            if (result.Code == StatusCodes.Status201Created)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, _mapper.Map<Models.SellerDto>(result.Data));
            }

            return new JsonResult(result.Message)
            {
                StatusCode = result.Code
            };
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Models.SellerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [Produces("application/json")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] UpdateSellerRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new SellerServiceProto.SellerServiceProtoClient(channel);
            var result = await client.UpdateAsync(_mapper.Map<UpdateSellerCommand>(request));

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.SellerDto>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        [HttpPatch("{id}/deactivate")]
        [ProducesResponseType(typeof(Models.SellerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [Produces("application/json")]
        public async Task<IActionResult> PatchAsync(Guid id, [FromBody] DeactivateSellerRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new SellerServiceProto.SellerServiceProtoClient(channel);
            var result = await client.DeactivateAsync(_mapper.Map<DeactivateSellerCommand>(request));

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.SellerDto>(result.Data))
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
            var client = new SellerServiceProto.SellerServiceProtoClient(channel);
            var result = await client.DeleteAsync(new DeleteSellerCommand { Id = id.ToString() });

            return new JsonResult(result.Message)
            {
                StatusCode = result.Code
            };
        }

        #endregion
    }
}
