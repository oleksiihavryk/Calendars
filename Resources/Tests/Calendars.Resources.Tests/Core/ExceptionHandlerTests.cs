using Calendars.Resources.Core;
using Microsoft.Extensions.Logging;

namespace Calendars.Resources.Tests.Core;
internal class ExceptionHandlerTests
{
    public async Task HandleAsync_HandleException_InnerMessageIsCorrect()
    {
        //arrange
        var message = string.Empty;
        var loggerMock = new Mock<ILogger<ExceptionHandler>>();
        loggerMock
            .Setup(l => l.LogCritical(
                It.IsAny<Exception?>(),
                It.IsAny<string?>()))
            .Callback((Exception e, string m) =>
            {
                message = m;
            });
        var logger = loggerMock.Object;
        var exceptionHandler = new ExceptionHandler(logger);
        var exception = new Exception();
        //act
        await exceptionHandler.HandleAsync(exception);
        //assert
        Assert.Equal(
            expected: "Unknown error has occurred in system.",
            actual: message);
    }
}