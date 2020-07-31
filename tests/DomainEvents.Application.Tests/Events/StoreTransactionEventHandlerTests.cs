using AutoFixture;
using DomainEvents.Application.Events;
using DomainEvents.Application.Tests.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DomainEvents.Application.Tests.Events
{
    public class StoreTransactionEventHandlerTests
    {
        private Mock<ILogger<StoreTransactionEventHandler>> mockLogger;

        [Fact]
        public async Task Handle_WhenCalled_LogsInfo()
        {
            var sut = Setup();

            var storeTransactionEvent = new Fixture().Create<StoreTransactionEvent>();
            await sut.Handle(storeTransactionEvent, default);

            mockLogger.VerifyLogInformation("StoreTransactionEventHandler - called...");
        }

        private StoreTransactionEventHandler Setup()
        {
            mockLogger = new Mock<ILogger<StoreTransactionEventHandler>>();

            var sut = new StoreTransactionEventHandler(mockLogger.Object);

            return sut;
        }
    }
}
