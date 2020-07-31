using AutoFixture;
using DomainEvents.Application.Commands;
using FluentAssertions;
using Xunit;

namespace DomainEvents.Application.Tests.Commands
{
    public class MakePurchaseCommandTests
    {
        [Fact]
        public void TransactionAmount_ReturnsCorrectValue()
        {
            var sut = new Fixture().Create<MakePurchaseCommand>();

            var expectedTransactionAmount = sut.ItemCost * sut.Quantity;

            sut.TransactionAmount.Should().Be(expectedTransactionAmount);
        }
    }
}
