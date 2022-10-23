using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PriceAnalytics.ApiGateway.Models;
using PriceAnalytics.ApiGateway.Proposal;
using System.Net;

namespace PriceAnalytics.ApiGateway.Controllers
{
    [Route("api/v1/catalog/proposal")]
    [ApiController]
    public class ProposalController : Controller
    {
        #region Fields

        private readonly ILogger<ProposalController> _logger;
        private readonly IMapper _mapper;
        private readonly string _catalogApiUrl = Environment.GetEnvironmentVariable("PA_CATALOG_URL");

        #endregion

        #region Constructor

        public ProposalController(
            ILogger<ProposalController> logger,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Actions

        [HttpGet]
        [ProducesResponseType(typeof(Models.PaginatedList<Models.ProposalDto>), (int)System.Net.HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string? name, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProposalServiceProto.ProposalServiceProtoClient(channel);
            var result = await client.GetAsync(new GetProposalsQuery
            {
                Name = name ?? "",
                Limit = limit,
                Offset = offset
            });

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.PaginatedList<Models.ProposalDto>>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        /// <summary>
        /// Gets a specific Proposal  by the supplied definition Id.
        /// </summary>
        /// <param name="id">System generated ID returned when create a Proposal.</param>
        /// <returns>
        /// A <see cref="Models.ProposalDto" /> which matches the input id.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Models.ProposalDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProposalServiceProto.ProposalServiceProtoClient(channel);
            var result = await client.GetByIdAsync(new GetProposalByIdQuery
            {
                Id = id.ToString()
            });

            return result.Code == StatusCodes.Status200OK
                ? Ok(_mapper.Map<Models.ProposalDto>(result.Data))
                : new JsonResult(result.Message)
                {
                    StatusCode = result.Code
                };
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.ProposalDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)System.Net.HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProposalRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_catalogApiUrl);
            var client = new ProposalServiceProto.ProposalServiceProtoClient(channel);
            var result = await client.CreateAsync(_mapper.Map<CreateProposalCommand>(request));

            if (result.Code == StatusCodes.Status201Created)
            {
                return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, _mapper.Map<Models.ProposalDto>(result.Data));
            }

            return new JsonResult(result.Message)
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
            var client = new ProposalServiceProto.ProposalServiceProtoClient(channel);
            var result = await client.DeleteAsync(new DeleteProposalCommand { Id = id.ToString() });

            return new JsonResult(result.Message)
            {
                StatusCode = result.Code
            };
        }
        #endregion
    }
}
