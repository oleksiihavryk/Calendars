using System.Net;
using Calendars.Resources.Core;
using Calendars.Resources.Core.Interfaces;
using Calendars.Resources.Middleware;
using Microsoft.AspNetCore.Http;

namespace Calendars.Resources.Tests.Middleware;
public class ExceptionHandlerMiddlewareTests
{
    private readonly HttpContext _httpContext = new DefaultHttpContext { Response = { StatusCode = 200 } };
    private readonly IResponseFactory _responseFactory;
    private readonly IExceptionHandler _exceptionHandler;

    public ExceptionHandlerMiddlewareTests()
    {
        var exceptionHandlerMock = new Mock<IExceptionHandler>();
        exceptionHandlerMock
            .Setup(eh => eh.HandleAsync(
                It.IsAny<Exception>()))
            .Returns(Task.CompletedTask);
        _exceptionHandler = exceptionHandlerMock.Object;
        var responseFactoryMock = new Mock<IResponseFactory>();
        responseFactoryMock
            .Setup(rf => rf.CreateResponse(
                It.IsAny<bool>(),
                It.IsAny<HttpStatusCode>(),
                It.IsAny<object?>(),
                It.IsAny<string[]>()))
            .Returns(new Response() { StatusCode = 500 });
        _responseFactory = responseFactoryMock.Object;
    }

    [Fact]
    public async Task InvokeAsync_ExceptionIsNotOccurred_StatusCodeIsNotChange()
    {
        //arrange
        Task Next(HttpContext ctx)
        {
            return Task.CompletedTask;
        }
        RequestDelegate next = Next;
        //act
        var middleware = new ExceptionHandlerMiddleware(_exceptionHandler, _responseFactory);
        await middleware.InvokeAsync(_httpContext, next);
        //assert
        Assert.Equal(
            expected: 200,
            actual: _httpContext.Response.StatusCode);
    }
    [Fact]
    public async Task InvokeAsync_ExceptionIsOccurred_StatusCodeIs500()
    {
        //arrange
        Task Next(HttpContext ctx)
        {
            return Task.FromException(new Exception());
        }
        RequestDelegate next = Next;
        //act
        var middleware = new ExceptionHandlerMiddleware(_exceptionHandler, _responseFactory);
        await middleware.InvokeAsync(_httpContext, next);
        //assert
        Assert.Equal(
            expected: 500,
            actual: _httpContext.Response.StatusCode);
    }
    [Fact]
    public async Task InvokeAsync_ExceptionIsOccurred_ContentTypeIsJson()
    {
        //arrange
        Task Next(HttpContext ctx)
        {
            return Task.FromException(new Exception());
        }
        RequestDelegate next = Next;
        //act
        var middleware = new ExceptionHandlerMiddleware(_exceptionHandler, _responseFactory);
        await middleware.InvokeAsync(_httpContext, next);
        //assert
        Assert.Equal(
            expected: "application/json; charset=utf-8",
            actual: _httpContext.Response.ContentType);
    }
}