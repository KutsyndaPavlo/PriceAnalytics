using FluentValidation;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.CreateSeller
{
    public class CreateSellerCommandValidator : AbstractValidator<CreateSellerCommand>
    {
        public CreateSellerCommandValidator(IApplicationDbContext applicationDbContext)
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
