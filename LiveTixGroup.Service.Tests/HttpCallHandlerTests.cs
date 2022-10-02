using System.Net;
using System.Text.Json;
using System.Xml.XPath;
using AutoFixture;
using AutoMapper;
using LiveTixGroup.Models;
using LiveTixGroup.Service.Mappers;
using Moq;
using Moq.Protected;
using Xunit;

namespace LiveTixGroup.Service.Tests;

public class HttpCallHandlerTests
{

	private Mock<IHttpClientFactory> _moqHttpClientFactory;
	private Mock<HttpMessageHandler> _moqHttpMessageHandler;
	private Fixture _fixture;
	private IHttpCallHandler _sut;
	
	public HttpCallHandlerTests()
	{
		_moqHttpClientFactory = new Mock<IHttpClientFactory>();
		_moqHttpMessageHandler = new Mock<HttpMessageHandler>();
		_fixture = new Fixture();

		var client = new HttpClient(_moqHttpMessageHandler.Object);
		client.BaseAddress = _fixture.Create<Uri>();
		_moqHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

		_sut = new HttpCallHandler(_moqHttpClientFactory.Object);
	}

	[Fact]
	public async Task Given_GetFromApiRequest_When_AlbumsAreRequested_Then_ReturnsExpectedResult()
	{
		//Assign
		var stubApiResponse = _fixture.Create<object>();
		var stubApiResponse1 = _fixture.Create<object>();
		var stubApiResponse2 = _fixture.Create<object>();
		var stubApiResponseContent = new List<object>
		{
			stubApiResponse,
			stubApiResponse1,
			stubApiResponse2
		};
		
		_moqHttpMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>
			(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>()
			)
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent($"{JsonSerializer.Serialize(stubApiResponseContent)}"),
			});
		
		//Act
		var result = await _sut.GetMetaData<AlbumResponse>(_fixture.Create<string>());

		//Assert
		_moqHttpMessageHandler.Protected()
			.Verify
			(
				"SendAsync",
				Times.Exactly(1),
				ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
				ItExpr.IsAny<CancellationToken>()
			);

		Assert.Equal(stubApiResponseContent.Count, result.Count);
	}
}