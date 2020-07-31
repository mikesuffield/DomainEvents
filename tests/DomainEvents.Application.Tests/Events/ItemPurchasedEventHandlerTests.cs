using AutoFixture;
using DomainEvents.Application.Events;
using DomainEvents.Application.Tests.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DomainEvents.Application.Tests.Events
{
    public class ItemPurchasedEventHandlerTests
    {
        private Mock<ILogger<ItemPurchasedEventHandler>> mockLogger;

        [Fact]
        public async Task Handle_WhenCalled_LogsInfo()
        {
            var sut = Setup();

            var itemPurchasedEvent = new Fixture().Create<ItemPurchasedEvent>();
            await sut.Handle(itemPurchasedEvent, default);

            mockLogger.VerifyLogInformation("ItemPurchasedEventHandler - called...");
        }

        private ItemPurchasedEventHandler Setup()
        {
            mockLogger = new Mock<ILogger<ItemPurchasedEventHandler>>();

            var sut = new ItemPurchasedEventHandler(mockLogger.Object);

            return sut;
        }
    }
}
