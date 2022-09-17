using AutoMapper;
using FluentValidation;
using Grpc.Core;
using MediatR;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Administration.Services.Proposal;

namespace PriceAnalytics.Catalog.API.Services
{
    public class ProposalService : ProposalServiceProto.ProposalServiceProtoBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProposalService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProposalService(
            IMediator mediator,
            ILogger<ProposalService> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        public async override Task<CreateProposalResult> Create(CreateProposalCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Proposal.Commands.CreateProposal.CreateProposalCommand>(request);
                var result = await _mediator.Send(command);

                return new CreateProposalResult
                {
                    Code = StatusCodes.Status201Created,
                    Data = _mapper.Map<ProposalDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException || ex is ProposalNotFoundException)
            {
                // ToDo logging
                return new CreateProposalResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new CreateProposalResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<GetProposalsResult> Get(GetProposalsQuery request, ServerCallContext context)
        {
            try
            {
                var query = _mapper.Map<PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposals.GetProposalsQuery>(request);
                var result = await _mediator.Send(query);

                return new GetProposalsResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<ProposalPaginatedList>(result)
                };
            }
            catch (Exception ex)
            {
                // ToDo logging

                return new GetProposalsResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<GetProposalByIdResult> GetById(GetProposalByIdQuery request, ServerCallContext context)
        {
            try
            {
                var query = _mapper.Map<PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById.GetProposalByIdQuery>(request);
                var result = await _mediator.Send(query);

                return new GetProposalByIdResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<ProposalDto>(result)
                };
            }
            catch (Exception ex) when (ex is ProposalNotFoundException)
            {
                return new GetProposalByIdResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging

                return new GetProposalByIdResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<DeleteProposalResult> Delete(DeleteProposalCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Proposal.Commands.DeleteProposal.DeleteProposalCommand>(request);
                var result = await _mediator.Send(command);

                return new DeleteProposalResult
                {
                    Code = StatusCodes.Status204NoContent,
                    Message = "Successfully deleted",
                    Data = result
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new DeleteProposalResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is ProposalNotFoundException)
            {
                return new DeleteProposalResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new DeleteProposalResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }
    }
}
