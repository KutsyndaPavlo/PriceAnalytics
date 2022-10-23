using FluentValidation;
using PriceAnalytics.Catalog.Application.Common.Interfaces;

namespace PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IApplicationDbContext applicationDbContext)
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
