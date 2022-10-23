using AutoMapper;
using FluentValidation;
using Grpc.Core;
using MediatR;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Common;
using PriceAnalytics.Administration.Services.Product;

namespace PriceAnalytics.Administration.Services.Services
{
    public class ProductService : ProductServiceProto.ProductServiceProtoBase
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public ProductService(
            IMediator mediator,
            ILogger<ProductService> logger,
            IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        public async override Task<GetProductsResult> Get(GetProductsQuery request, ServerCallContext context)
        {
            try
            {
                var query = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Queries.GetProducts.GetProductsQuery>(request);
                var result = await _mediator.Send(query);

                return new GetProductsResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<ProductPaginatedList>(result)
                };
            }
            catch (Exception ex)
            {
                // ToDo logging

                return new GetProductsResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<GetProductByIdResult> GetById(GetProductByIdQuery request, ServerCallContext context)
        {
            try
            {
                var query = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Queries.GetProductById.GetProductByIdQuery>(request);
                var result = await _mediator.Send(query);

                return new GetProductByIdResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<ProductDto>(result)
                };
            }
            catch (Exception ex) when (ex is ProductNotFoundException)
            {
                return new GetProductByIdResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message                    
                };
            }
            catch (Exception ex)
            {
                // ToDo logging

                return new GetProductByIdResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<CreateProductResult> Create(CreateProductCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct.CreateProductCommand>(request);
                var result = await _mediator.Send(command);

                return new CreateProductResult
                {
                    Code = StatusCodes.Status201Created,
                    Data = _mapper.Map<ProductDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new CreateProductResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new CreateProductResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<UpdateProductResult> Update(UpdateProductCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct.UpdateProductCommand>(request);
                var result = await _mediator.Send(command);

                return new UpdateProductResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<ProductDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new UpdateProductResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is ProductNotFoundException)
            {
                return new UpdateProductResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is ProductConcurrentUpdateException)
            {
                return new UpdateProductResult
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new UpdateProductResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<DeactivateProductResult> Deactivate(DeactivateProductCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Commands.DeactivateProduct.DeactivateProductCommand>(request);
                var result = await _mediator.Send(command);

                return new DeactivateProductResult
                {
                    Code = StatusCodes.Status200OK,
                    Data = _mapper.Map<ProductDto>(result)
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new DeactivateProductResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is ProductNotFoundException)
            {
                return new DeactivateProductResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is ProductConcurrentUpdateException)
            {
                return new DeactivateProductResult
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new DeactivateProductResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }

        public async override Task<DeleteProductResult> Delete(DeleteProductCommand request, ServerCallContext context)
        {
            try
            {
                var command = _mapper.Map<PriceAnalytics.Catalog.Application.Product.Commands.DeleteProduct.DeleteProductCommand>(request);
                var result = await _mediator.Send(command);

                return new DeleteProductResult
                {
                    Code = StatusCodes.Status204NoContent,
                    Message = "Successfully deleted",
                    Data = result
                };
            }
            catch (Exception ex) when (ex is BusinessRuleValidationException || ex is ValidationException)
            {
                // ToDo logging
                return new DeleteProductResult
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                };
            }
            catch (Exception ex) when (ex is ProductNotFoundException)
            {
                return new DeleteProductResult
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                // ToDo logging
                return new DeleteProductResult
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = $"Something went wrong. details: {ex}"
                };
            }
        }
    }
}
