using AutoFixture;
using AutoMapper;
using DomainEvents.Application.Commands;
using DomainEvents.Application.Events;
using DomainEvents.Application.Profiles;
using FluentAssertions;
using Xunit;

namespace DomainEvents.Application.Tests.Profiles
{
    public class StoreTransactionEventProfileTests
    {
        [Fact]
        public void MakePurchaseCommand_to_StoreTransactionEvent()
        {
            var makePurchaseCommand = new Fixture().Create<MakePurchaseCommand>();

            var expected = new StoreTransactionEvent(makePurchaseCommand.StoreId,
                makePurchaseCommand.TransactionAmount,
                makePurchaseCommand.TimeStamp);

            var mapper = SetupMapper();
            var output = mapper.Map<StoreTransactionEvent>(makePurchaseCommand);

            output.Should().BeEquivalentTo(expected);
        }

        private IMapper SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StoreTransactionEventProfile>();
            });

            var mapper = mapperConfig.CreateMapper();

            return mapper;
        }
    }
}
