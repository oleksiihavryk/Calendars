using Calendars.Resources.ActionResults;
using Calendars.Resources.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Calendars.Resources.Core;

namespace Calendars.Resources.Tests.ResponseResults;
public class ResponseBadRequestResultTests
{
    private Dictionary<string, IEnumerable<string>> _parameterNameAndErrors = 
        new Dictionary<string, IEnumerable<string>>()
        {
            ["Param"] = new[] { "error1", "error2" }
        };

    [Fact]
    public async Task ExecuteResultAsync_ResultReceivedCorrect_StatusCodeIs400()
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
                StatusCode = 201,
            });
        var responseFactory = responseFactoryMock.Object;
        var actionContext = new ActionContext
        {
            HttpContext = new DefaultHttpContext()
        };

        //act
        var responseCreatedResult = new ResponseBadRequestResult(
            responseFactory: responseFactory,
            _parameterNameAndErrors);

        await responseCreatedResult.ExecuteResultAsync(actionContext);
        //assert
        Assert.Equal(
            expected: 201,
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
                StatusCode = 201,
            });
        var responseFactory = responseFactoryMock.Object;
        var actionContext = new ActionContext
        {
            HttpContext = new DefaultHttpContext()
        };

        //act
        var responseCreatedResult = new ResponseCreatedResult(
            responseFactory: responseFactory,
            _parameterNameAndErrors);

        await responseCreatedResult.ExecuteResultAsync(actionContext);
        //assert
        Assert.Equal(
            expected: "application/json; charset=utf-8",
            actual: actionContext.HttpContext.Response.ContentType);
    }
}