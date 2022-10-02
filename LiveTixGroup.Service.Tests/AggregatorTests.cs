using AutoFixture;
using AutoMapper;
using LiveTixGroup.Models;
using LiveTixGroup.Service.Mappers;
using Moq;
using Xunit;

namespace LiveTixGroup.Service.Tests;

public class AggregatorTests
{

	private IMapper _mapper;
	private Fixture _fixture;
	private IAggregator _sut;
	
	public AggregatorTests()
	{
		_mapper = new MapperConfiguration(
			x =>
			{
				x.AddProfile(new LtgEntitiesMapping());
			}).CreateMapper();

		_fixture = new Fixture();

		_sut = new Aggregator(_mapper);
	}

	[Fact]
	public async Task Given_PhotoAlbum_When_NoUserIdFilterIsProvided_Then_ReturnsAllCombinedResults()
	{
		//Assign
		var album1 = _fixture.Build<AlbumResponse>()
			.With(x => x.UserId, 1)
			.With(x => x.Id, 1)
			.Create();
		var album2 = _fixture.Build<AlbumResponse>()
			.With(x => x.UserId, 1)
			.With(x => x.Id, 2)
			.Create();
		var album3 = _fixture.Build<AlbumResponse>()
			.With(x => x.UserId, 1)
			.With(x => x.Id, 3)
			.Create();
		var stubAlbumList = new List<AlbumResponse>
		{
			album1,
			album2,
			album3
		};

		var stubPhotoList = new List<PhotoResponse>
		{
			_fixture.Build<PhotoResponse>()
				.With(x => x.AlbumId, 1)
				.With(x => x.Id, 1)
				.Create(),

			_fixture.Build<PhotoResponse>()
				.With(x => x.AlbumId, 1)
				.With(x => x.Id, 2)
				.Create(),

			_fixture.Build<PhotoResponse>()
				.With(x => x.AlbumId, 2)
				.With(x => x.Id, 3)
				.Create(),
		};

		var expectedResult = new List<PhotoAlbumAggregatedModel>
		{
			new()
			{
				UserId = 1,
				AlbumModels = new List<AlbumModel>
				{
					new()
					{
						Id = 1,
						Title = album1.Title,
					}
				}
			}
		};

		//Act
		var result = _sut.Aggregate(0,stubPhotoList, stubAlbumList);
		
		//Assert
		Assert.Equal(expectedResult.Count, result.Count);
	}
}