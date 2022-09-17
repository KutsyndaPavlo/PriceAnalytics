using FluentValidation;

namespace PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
