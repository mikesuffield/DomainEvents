using AutoFixture;
using DomainEvents.Api.Validators;
using DomainEvents.Api.ViewModels;
using FluentValidation.TestHelper;
using Xunit;

namespace DomainEvents.Api.Tests.Validators
{
    public class PurchaseViewModelValidatorTests
    {
        [Fact]
        public void Validator_WhenCalledWithValidViewModel_DoesNotThrowException()
        {
            var purchaseViewModel = new Fixture().Create<PurchaseViewModel>();

            var sut = new PurchaseViewModelValidator();

            var result = sut.TestValidate(purchaseViewModel);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_WhenInvalidItemIdProvided_ThrowsException()
        {
            var purchaseViewModel = new Fixture().Build<PurchaseViewModel>().Without(x => x.ItemId).Create();

            var sut = new PurchaseViewModelValidator();

            var result = sut.TestValidate(purchaseViewModel);

            result.ShouldHaveValidationErrorFor(x => x.ItemId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validator_WhenInvalidItemCostProvided_ThrowsException(object itemCost)
        {
            var purchaseViewModel = new Fixture().Build<PurchaseViewModel>().With(x => x.ItemCost, itemCost).Create();

            var sut = new PurchaseViewModelValidator();

            var result = sut.TestValidate(purchaseViewModel);

            result.ShouldHaveValidationErrorFor(x => x.ItemCost);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validator_WhenInvalidQuantityProvided_ThrowsException(object quantity)
        {
            var purchaseViewModel = new Fixture().Build<PurchaseViewModel>().With(x => x.Quantity, quantity).Create();

            var sut = new PurchaseViewModelValidator();

            var result = sut.TestValidate(purchaseViewModel);

            result.ShouldHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Validator_WhenInvalidStoreIdProvided_ThrowsException()
        {
            var purchaseViewModel = new Fixture().Build<PurchaseViewModel>().Without(x => x.StoreId).Create();

            var sut = new PurchaseViewModelValidator();

            var result = sut.TestValidate(purchaseViewModel);

            result.ShouldHaveValidationErrorFor(x => x.StoreId);
        }
    }
}
