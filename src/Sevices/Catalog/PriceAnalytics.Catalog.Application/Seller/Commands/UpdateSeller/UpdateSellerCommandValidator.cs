using FluentValidation;

namespace PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller
{
    public class UpdateSellerCommandValidator : AbstractValidator<UpdateSellerCommand>
    {
        public UpdateSellerCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty();
        }
    }
}
