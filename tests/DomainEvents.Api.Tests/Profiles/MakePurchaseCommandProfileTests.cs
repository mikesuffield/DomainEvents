using AutoFixture;
using AutoMapper;
using DomainEvents.Api.Profiles;
using DomainEvents.Api.ViewModels;
using DomainEvents.Application.Commands;
using FluentAssertions;
using Xunit;

namespace DomainEvents.Api.Tests.Profiles
{
    public class MakePurchaseCommandProfileTests
    {
        [Fact]
        public void PurchaseViewModel_to_MakePurchaseCommand()
        {
            var purchaseViewModel = new Fixture().Create<PurchaseViewModel>();

            var expected = new MakePurchaseCommand(purchaseViewModel.ItemId,
                purchaseViewModel.ItemCost,
                purchaseViewModel.Quantity,
                purchaseViewModel.StoreId,
                purchaseViewModel.TimeStamp);

            var mapper = SetupMapper();
            var output = mapper.Map<MakePurchaseCommand>(purchaseViewModel);

            output.Should().BeEquivalentTo(expected);
        }

        private IMapper SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MakePurchaseCommandProfile>();
            });

            var mapper = mapperConfig.CreateMapper();

            return mapper;
        }
    }
}
