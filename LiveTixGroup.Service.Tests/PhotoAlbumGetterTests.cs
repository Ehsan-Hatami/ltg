using System.Collections;
using AutoFixture;
using LiveTixGroup.Models;
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
		var stubAlbumList = new List<AlbumResponse>
		{
			_fixture.Create<AlbumResponse>(),
			_fixture.Create<AlbumResponse>(),
			_fixture.Create<AlbumResponse>()
		};

		var stubPhotoList = new List<PhotoResponse>
		{
			_fixture.Create<PhotoResponse>(),
			_fixture.Create<PhotoResponse>(),
			_fixture.Create<PhotoResponse>(),
			_fixture.Create<PhotoResponse>(),
			_fixture.Create<PhotoResponse>(),
			_fixture.Create<PhotoResponse>()
		};

		var expectedResult = _fixture.Create<IList<PhotoAlbumAggregatedModel>>();

		var stubUserId = _fixture.Create<int>();

		_mockHttpCallHandler.Setup(x => x.GetMetaData<PhotoResponse>("photos"))
			.ReturnsAsync(stubPhotoList);

		_mockHttpCallHandler.Setup(x => x.GetMetaData<AlbumResponse>("albums"))
			.ReturnsAsync(stubAlbumList);

		_mockAggregator.Setup(x => x.Aggregate(stubUserId, stubPhotoList, stubAlbumList)).Returns(expectedResult);

		//Act
		var result = await _sut.Get(stubUserId
		);
		
		//Assert
		Assert.Equal(expectedResult.Count, result.Count);
		
		_mockHttpCallHandler.Verify(x => x.GetMetaData<PhotoResponse>("photos"), Times.Once);
		_mockHttpCallHandler.Verify(x => x.GetMetaData<AlbumResponse>("albums"), Times.Once);
		_mockAggregator.Verify(x => x.Aggregate(stubUserId, stubPhotoList, stubAlbumList), Times.Once);
	}
}