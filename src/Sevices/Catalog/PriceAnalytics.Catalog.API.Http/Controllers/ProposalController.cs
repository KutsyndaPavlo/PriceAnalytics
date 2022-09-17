using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.Catalog.API.Models;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct;
using PriceAnalytics.Catalog.Application.Proposal.Commands.CreateProposal;
using PriceAnalytics.Catalog.Application.Proposal.Commands.DeleteProposal;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposals;

namespace PriceAnalytics.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProposalController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProposalController(
            IMediator mediator,
            ILogger<ProposalController> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions

        [HttpGet]
        //[SwaggerOperation(Tags = new[] { "Proposal" }, Summary = "Get all Proposals.")]
        //[Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(IEnumerable<ProposalDto>))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAsync([FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var query = new GetProposalsQuery(offset, limit);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific Proposal  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a Proposal.</param>
        /// <returns>
        /// A <see cref="ProposalDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        // [SwaggerOperation(Tags = new[] { "Proposal" }, Summary = "Get Proposal by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProposalDto))] 
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "RProposal does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetProposalByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        //[SwaggerOperation(Tags = new[] { "Proposal" }, Summary = "Create Proposal.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Success", Type = typeof(ProposalDto))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status409Conflict, "Proposal already exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProposalRequest request)
        {
            var result = await _mediator.Send(_mapper.Map<CreateProposalCommand>(request));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
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
            var result = await _mediator.Send(new DeactivateProposalCommand { Id = id });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[SwaggerOperation(Tags = new[] { "Proposal" }, Summary = "Delete Proposal by id.")]
        [Produces("application/json")]
        //[SwaggerResponse(StatusCodes.Status204NoContent)]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request, Validation error")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Proposal does not exist")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProposalCommand { Id = id };

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
