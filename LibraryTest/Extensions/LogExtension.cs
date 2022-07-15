using Microsoft.Extensions.Logging;
using Moq;
using System.Text.RegularExpressions;

namespace LibraryTest.Extensions
{
    public static class LogExtension
    {
        public static void VerifyLog<T>(this Mock<ILogger<T>> logger, LogLevel level, Times times, string? message = null) =>
            logger.Verify(x => x.Log(
                level
                , It.IsAny<EventId>()
                , It.Is<It.IsAnyType>((x, y) => message == null || Regex.IsMatch(x.ToString(), message))
                , It.IsAny<Exception>()
                , (Func<object, Exception, string>)It.IsAny<object>()), times);
    }
}
