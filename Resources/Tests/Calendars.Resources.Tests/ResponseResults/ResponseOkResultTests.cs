using Calendars.Resources.ActionResults;
using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Calendars.Resources.Core;

namespace Calendars.Resources.Tests.ResponseResults;
public class ResponseOkResultTests
{
    private readonly object? _obj = new object();
    private readonly string[] _messages = new[] { string.Empty, string.Empty };

    [Fact]
    public async Task ExecuteResultAsync_ResultReceivedCorrect_StatusCodeIs200()
    {
        //arrange
        var responseFactoryMock = new Mock<IResponseFactory>();
        responseFactoryMock.Setup(rf => rf.CreateResponse(
                It.IsAny<bool>(),
                It.IsAny<HttpStatusCode>(),
                It.IsAny<object?>(),
                It.IsAny<string[]>()))
            .Returns(() => new Response()
            {
                StatusCode = 200,
            });
        var responseFactory = responseFactoryMock.Object;
        var actionContext = new ActionContext
        {
            HttpContext = new DefaultHttpContext()
        };

        //act
        var responseCreatedResult = new ResponseCreatedResult(
            responseFactory: responseFactory,
            value: _obj,
            messages: _messages);

        await responseCreatedResult.ExecuteResultAsync(actionContext);
        //assert
        Assert.Equal(
            expected: 200,
            actual: actionContext.HttpContext.Response.StatusCode);
    }
    [Fact]
    public async Task ExecuteResultAsync_ResultReceivedCorrect_ContentTypeIsJson()
    {
        //arrange
        var responseFactoryMock = new Mock<IResponseFactory>();
        responseFactoryMock.Setup(rf => rf.CreateResponse(
                It.IsAny<bool>(),
                It.IsAny<HttpStatusCode>(),
                It.IsAny<object?>(),
                It.IsAny<string[]>()))
            .Returns(() => new Response()
            {
                StatusCode = 200,
            });
        var responseFactory = responseFactoryMock.Object;
        var actionContext = new ActionContext
        {
            HttpContext = new DefaultHttpContext()
        };

        //act
        var responseCreatedResult = new ResponseCreatedResult(
            responseFactory: responseFactory,
            value: _obj,
            messages: _messages);

        await responseCreatedResult.ExecuteResultAsync(actionContext);
        //assert
        Assert.Equal(
            expected: "application/json; charset=utf-8",
            actual: actionContext.HttpContext.Response.ContentType);
    }
}