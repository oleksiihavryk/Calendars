using System.Net;
using Calendars.Resources.Core;

namespace Calendars.Resources.Tests.Core;
public class ResponseFactoryTests
{
    private readonly ResponseFactory _responseFactory = new ResponseFactory();

    [Fact]
    public void CreateResponse_IsSuccessEqualInPassedParameterAndResponseObject_ReturnsTrue()
    {
        //arrange
        const bool isSuccess = true;
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const object? result = null;
        var messages = new[] { "", "" };
        //act
        var response = _responseFactory.CreateResponse(isSuccess, statusCode, result, messages);
        //assert
        Assert.Equal(
            expected: isSuccess,
            actual: response.IsSuccess);
    }
    [Fact]
    public void CreateResponse_StatusCodeEqualInPassedParameterAndResponseObject_ReturnsTrue()
    {
        //arrange
        const bool isSuccess = true;
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const object? result = null;
        var messages = new[] { "", "" };
        //act
        var response = _responseFactory.CreateResponse(isSuccess, statusCode, result, messages);
        //assert
        Assert.Equal(
            expected: (int)statusCode,
            actual: response.StatusCode);
    }
    [Fact]
    public void CreateResponse_ResultEqualInPassedParameterAndResponseObject_ReturnsTrue()
    {
        //arrange
        const bool isSuccess = true;
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const object? result = null;
        var messages = new[] { "", "" };
        //act
        var response = _responseFactory.CreateResponse(isSuccess, statusCode, result, messages);
        //assert
        Assert.Equal(
            expected: result,
            actual: response.Result);
    }
    [Fact]
    public void CreateResponse_MessagesEqualInPassedParameterAndResponseObject_ReturnsTrue()
    {
        //arrange
        const bool isSuccess = true;
        const HttpStatusCode statusCode = HttpStatusCode.OK;
        const object? result = null;
        var messages = new[] { "", "" };
        //act
        var response = _responseFactory.CreateResponse(isSuccess, statusCode, result, messages);
        //assert
        Assert.Equal(
            expected: messages,
            actual: response.Messages);
    }
    [Fact]
    public void CreateResponse_PassedIncorrectHttpStatusCodeValue_ThrowException()
    {
        //arrange
        const bool isSuccess = true;
        const HttpStatusCode statusCode = (HttpStatusCode)40000;
        const object? result = null;
        var messages = new[] { "", "" };
        //act
        void Act()
        {
            var response = _responseFactory.CreateResponse(isSuccess, statusCode, result, messages);
        }
        //assert
        Assert.Throws<ArgumentException>(Act);
    }
}