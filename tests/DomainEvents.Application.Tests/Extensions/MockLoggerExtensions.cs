using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace DomainEvents.Application.Tests.Extensions
{
    public static class MockLoggerExtensions
    {
        public static void VerifyLogInformation<T>(this Mock<ILogger<T>> logger, string expectedLog)
        {
            logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(expectedLog, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}
