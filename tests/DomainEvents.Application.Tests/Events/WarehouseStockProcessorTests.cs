using AutoFixture;
using DomainEvents.Application.Events;
using DomainEvents.Application.Tests.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DomainEvents.Application.Tests.Events
{
    public class WarehouseStockProcessorTests
    {
        private Mock<ILogger<WarehouseStockProcessor>> mockLogger;

        [Fact]
        public async Task Handle_WhenCalled_LogsInfo()
        {
            var sut = Setup();

            var itemPurchasedEvent = new Fixture().Create<ItemPurchasedEvent>();
            await sut.Handle(itemPurchasedEvent, default);

            mockLogger.VerifyLogInformation("WarehouseStockProcessor - called...");
        }

        private WarehouseStockProcessor Setup()
        {
            mockLogger = new Mock<ILogger<WarehouseStockProcessor>>();

            var sut = new WarehouseStockProcessor(mockLogger.Object);

            return sut;
        }
    }
}
