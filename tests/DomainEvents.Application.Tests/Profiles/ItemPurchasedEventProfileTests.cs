using AutoFixture;
using AutoMapper;
using DomainEvents.Application.Commands;
using DomainEvents.Application.Events;
using DomainEvents.Application.Profiles;
using FluentAssertions;
using Xunit;

namespace DomainEvents.Api.Tests.Profiles
{
    public class ItemPurchasedEventProfileTests
    {
        [Fact]
        public void MakePurchaseCommand_to_ItemPurchasedEvent()
        {
            var makePurchaseCommand = new Fixture().Create<MakePurchaseCommand>();

            var expected = new ItemPurchasedEvent(makePurchaseCommand.ItemId,
                makePurchaseCommand.Quantity,
                makePurchaseCommand.TimeStamp);

            var mapper = SetupMapper();
            var output = mapper.Map<ItemPurchasedEvent>(makePurchaseCommand);

            output.Should().BeEquivalentTo(expected);
        }

        private IMapper SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ItemPurchasedEventProfile>();
            });

            var mapper = mapperConfig.CreateMapper();

            return mapper;
        }
    }
}
