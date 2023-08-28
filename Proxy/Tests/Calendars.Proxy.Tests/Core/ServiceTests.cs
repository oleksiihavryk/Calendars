using System.Net;
using Calendars.Proxy.Core;

namespace Calendars.Proxy.Tests.Core;
public class ServiceTests
{
    [Fact]
    public void Client_GetClient_ReturnsClientCreatedByPassedHttpClientFactory()
    {
        //arrange
        var httpClient = new HttpClient();
        var uri = new Uri("http://localhost:1");
        var httpClientFactory = new Mock<IHttpClientFactory>();
        httpClientFactory.Setup(hcf =>
                hcf.CreateClient(
                    It.IsAny<string>()))
            .Returns(httpClient);
        var service = new Service(httpClientFactory.Object, uri);
        //act
        var client = service.Client;
        //assert
        Assert.NotNull(client);
        Assert.IsType<HttpClient>(client);
        Assert.Equal(httpClient, client);
    }
}