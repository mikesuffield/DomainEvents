using AutoFixture;
using AutoMapper;
using DomainEvents.Application.Commands;
using DomainEvents.Application.Events;
using DomainEvents.Application.Tests.Extensions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DomainEvents.Application.Tests.Commands
{
    public class MakePurchaseCommandHandlerTests
    {
        private Mock<IMediator> mockMediator;
        private Mock<ILogger<MakePurchaseCommandHandler>> mockLogger;
        private readonly MakePurchaseCommand makePurchaseCommand = new Fixture().Create<MakePurchaseCommand>();

        [Fact]
        public async Task Handle_WhenCalled_LogsInformation()
        {
            var sut = Setup();

            await sut.Handle(makePurchaseCommand, default);

            mockLogger.VerifyLogInformation("MakePurchaseCommandHandler - called...");
        }

        [Fact]
        public async Task Handle_WhenCalled_PublishesEvents()
        {
            var sut = Setup();

            await sut.Handle(makePurchaseCommand, default);

            mockMediator.Verify(x => x.Publish(It.IsAny<ItemPurchasedEvent>(), default), Times.Once);
            mockMediator.Verify(x => x.Publish(It.IsAny<StoreTransactionEvent>(), default), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenCalled_ReturnsString()
        {
            var sut = Setup();

            var result = await sut.Handle(makePurchaseCommand, default);

            result.Should().Be("Thanks for your purchase. We are processing your order.");
        }

        private MakePurchaseCommandHandler Setup()
        {
            mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger<MakePurchaseCommandHandler>>();

            var sut = new MakePurchaseCommandHandler(mockMediator.Object, mockMapper.Object, mockLogger.Object);

            return sut;
        }
    }
}
