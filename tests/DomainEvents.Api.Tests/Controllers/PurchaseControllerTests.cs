using AutoFixture;
using AutoMapper;
using DomainEvents.Api.Controllers;
using DomainEvents.Api.ViewModels;
using DomainEvents.Application.Commands;
using FluentAssertions;
using MediatR;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DomainEvents.Api.Tests.Controllers
{
    public class PurchaseControllerTests
    {
        private Mock<IMediator> mockMediator;
        private readonly PurchaseViewModel purchaseViewModel = new Fixture().Create<PurchaseViewModel>();

        [Fact]
        public async Task Get_WhenCalled_SendsCommand()
        {
            var SUT = Setup();

            await SUT.Get(purchaseViewModel);

            mockMediator.Verify(x => x.Send(It.IsAny<MakePurchaseCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsString()
        {
            var SUT = Setup();

            var result = await SUT.Get(purchaseViewModel);

            result.Should().Be("Test string");
        }

        private PurchaseController Setup()
        {
            var mockMapper = new Mock<IMapper>();

            mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<MakePurchaseCommand>(), default))
                .Returns(Task.FromResult("Test string"));

            var SUT = new PurchaseController(mockMediator.Object, mockMapper.Object);

            return SUT;
        }
    }
}
