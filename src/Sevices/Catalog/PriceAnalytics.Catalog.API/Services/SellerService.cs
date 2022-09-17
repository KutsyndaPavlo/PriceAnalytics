using AutoMapper;
using FluentValidation;
using Grpc.Core;
using MediatR;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Administration.Services.Seller;

namespace PriceAnalytics.Catalog.API.Services
{
    public class SellerService : SellerServiceProto.SellerServiceProtoBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<SellerService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public SellerService(
            IMediator mediator,
            ILogger<SellerService> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        public async override Task<GetSellersResult> Get(GetSellersQuery request, ServerCallContext context)
        {
            try
            {
                var query = _mapper.Map<PriceAnalytics.Catalog.Application.Seller.Queries.GetSellers.GetSellersQuery>(request);
                var result = await _mediator.Send(query);

                return new GetSellersResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<SellerPaginatedList>(result)
                };
            }
            catch (Exception ex)
            {
                // ToDo logging

                return new GetSellersResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<GetSellerByIdResult> GetById(GetSellerByIdQuery request, ServerCallContext context)
        {
            try
            {
                var query = _mapper.Map<PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById.GetSellerByIdQuery>(request);
                var result = await _mediator.Send(query);

                return new GetSellerByIdResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<SellerDto>(result)
                };
            }
            catch (Exception ex) when (ex is SellerNotFoundException)
            {
                return new GetSellerByIdResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging

                return new GetSellerByIdResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<CreateSellerResult> Create(CreateSellerCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller.CreateSellerCommand>(request);
                var result = await _mediator.Send(command);

                return new CreateSellerResult
                {
                    Code = StatusCodes.Status201Created,
                    Data = _mapper.Map<SellerDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new CreateSellerResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new CreateSellerResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<UpdateSellerResult> Update(UpdateSellerCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand>(request);
                var result = await _mediator.Send(command);

                return new UpdateSellerResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<SellerDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new UpdateSellerResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is SellerNotFoundException)
            {
                return new UpdateSellerResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is SellerConcurrentUpdateException)
            {
                return new UpdateSellerResult
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new UpdateSellerResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<DeactivateSellerResult> Deactivate(DeactivateSellerCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Seller.Commands.DeactivateSeller.DeactivateSellerCommand>(request);
                var result = await _mediator.Send(command);

                return new DeactivateSellerResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<SellerDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new DeactivateSellerResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is SellerNotFoundException)
            {
                return new DeactivateSellerResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is SellerConcurrentUpdateException)
            {
                return new DeactivateSellerResult
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new DeactivateSellerResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<DeleteSellerResult> Delete(DeleteSellerCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Seller.Commands.DeleteSeller.DeleteSellerCommand>(request);
                var result = await _mediator.Send(command);

                return new DeleteSellerResult
                {
                    Code = StatusCodes.Status204NoContent,
                    Message = "Successfully deleted",
                    Data = result
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new DeleteSellerResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is SellerNotFoundException)
            {
                return new DeleteSellerResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new DeleteSellerResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }
    }
}
