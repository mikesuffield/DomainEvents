using DomainEvents.Api.ViewModels;
using FluentValidation;

namespace DomainEvents.Api.Validators
{
    public class PurchaseViewModelValidator : AbstractValidator<PurchaseViewModel>
    {
        public PurchaseViewModelValidator()
        {
            RuleFor(x => x.ItemId).NotEmpty();
            RuleFor(x => x.ItemCost).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.StoreId).NotEmpty();
        }
    }
}
