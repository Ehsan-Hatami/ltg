using System.Collections;
using AutoFixture;
using LiveTixGroup.Models;
using LiveTixGroup.Models.ExternalApiResponses;
using Moq;
using Xunit;

namespace LiveTixGroup.Service.Tests;

public class PhotoAlbumGetterTests
{
private Mock<IHttpCallHandler> _mockHttpCallHandler;
private Mock<IAggregator> _mockAggregator;
	private Fixture _fixture;
	private IPhotoAlbumGetter _sut;
	
	public PhotoAlbumGetterTests()
	{
		_mockHttpCallHandler = new Mock<IHttpCallHandler>();
		_mockAggregator = new Mock<IAggregator>();

		_fixture = new Fixture();
	
		_sut = new PhotoAlbumGetter(_mockHttpCallHandler.Object, _mockAggregator.Object);
	}

	[Fact]
	public async Task Given_PhotoAlbum_When_NoUserIdFilterIsProvided_Then_ReturnsAllCombinedResults()
	{
		//Assign
		var stubAlbumList = new List<AlbumApiResponse>
		{
			_fixture.Create<AlbumApiResponse>(),
			_fixture.Create<AlbumApiResponse>(),
			_fixture.Create<AlbumApiResponse>()
		};

		var stubPhotoList = new List<PhotoApiResponse>
		{
			_fixture.Create<PhotoApiResponse>(),
			_fixture.Create<PhotoApiResponse>(),
			_fixture.Create<PhotoApiResponse>(),
			_fixture.Create<PhotoApiResponse>(),
			_fixture.Create<PhotoApiResponse>(),
			_fixture.Create<PhotoApiResponse>()
		};

		var expectedResult = _fixture.Create<IList<PhotoAlbumAggregatedModel>>();

		var stubUserId = _fixture.Create<int>();

		_mockHttpCallHandler.Setup(x => x.GetMetaData<PhotoApiResponse>("photos"))
			.ReturnsAsync(stubPhotoList);

		_mockHttpCallHandler.Setup(x => x.GetMetaData<AlbumApiResponse>("albums"))
			.ReturnsAsync(stubAlbumList);

		_mockAggregator.Setup(x => x.Aggregate(stubUserId, stubPhotoList, stubAlbumList)).Returns(expectedResult);

		//Act
		var result = await _sut.Get(stubUserId
		);
		
		//Assert
		Assert.Equal(expectedResult.Count, result.Count);
		
		_mockHttpCallHandler.Verify(x => x.GetMetaData<PhotoApiResponse>("photos"), Times.Once);
		_mockHttpCallHandler.Verify(x => x.GetMetaData<AlbumApiResponse>("albums"), Times.Once);
		_mockAggregator.Verify(x => x.Aggregate(stubUserId, stubPhotoList, stubAlbumList), Times.Once);
	}
}