using AutoFixture;
using AutoMapper;
using LiveTixGroup.Models;
using LiveTixGroup.Models.ExternalApiResponses;
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

	[Theory]
	[InlineData(2)]
	[InlineData(1)]
	[InlineData(0)]
	public void Given_PhotoAlbum_When_NoUserIdFilterIsProvided_Then_ReturnsAllCombinedResults(int userId)
	{
		//Assign
		var album1 = _fixture.Build<AlbumApiResponse>()
			.With(x => x.UserId, 1)
			.With(x => x.Id, 1)
			.Create();
		var album2 = _fixture.Build<AlbumApiResponse>()
			.With(x => x.UserId, 1)
			.With(x => x.Id, 2)
			.Create();
		var album3 = _fixture.Build<AlbumApiResponse>()
			.With(x => x.UserId, 1)
			.With(x => x.Id, 3)
			.Create();
		var album4 = _fixture.Build<AlbumApiResponse>()
			.With(x => x.UserId, 2)
			.With(x => x.Id, 4)
			.Create();
		var album5 = _fixture.Build<AlbumApiResponse>()
			.With(x => x.UserId, 2)
			.With(x => x.Id, 5)
			.Create();

		var stubAlbumList = new List<AlbumApiResponse>
		{
			album1,
			album2,
			album3,
			album4,
			album5
		};

		var photo1 = _fixture.Build<PhotoApiResponse>()
			.With(x => x.AlbumId, 1)
			.With(x => x.Id, 1)
			.Create();

		var photo2 = _fixture.Build<PhotoApiResponse>()
			.With(x => x.AlbumId, 1)
			.With(x => x.Id, 2)
			.Create();

		var photo3 = _fixture.Build<PhotoApiResponse>()
			.With(x => x.AlbumId, 2)
			.With(x => x.Id, 3)
			.Create();

		var photo4 = _fixture.Build<PhotoApiResponse>()
			.With(x => x.AlbumId, 4)
			.With(x => x.Id, 4)
			.Create();

		var photo5 = _fixture.Build<PhotoApiResponse>()
			.With(x => x.AlbumId, 5)
			.With(x => x.Id, 5)
			.Create();

		var stubPhotoList = new List<PhotoApiResponse>
		{
			photo1,
			photo2,
			photo3,
			photo4,
			photo5
		};

		var fullResult = new List<PhotoAlbumAggregatedModel>
		{
			new()
			{
				UserId = 1,
				AlbumModels = new List<AlbumModel>
				{
					new()
					{
						Id = album1.Id,
						Title = album1.Title,
						PhotoModels = new List<PhotoModel>
						{
							new()
							{
								Id = photo1.Id,
								ThumbnailUrl = photo1.ThumbnailUrl,
								Title = photo1.Title,
								Url = photo1.Url
							},
							new()
							{
								Id = photo2.Id,
								ThumbnailUrl = photo2.ThumbnailUrl,
								Title = photo2.Title,
								Url = photo2.Url
							}
						}
					},
					new()
					{
						Id = album2.Id,
						Title = album2.Title,
						PhotoModels = new List<PhotoModel>
						{
							new()
							{
								Id = photo3.Id,
								ThumbnailUrl = photo3.ThumbnailUrl,
								Title = photo3.Title,
								Url = photo3.Url
							}
						}
					},
					new()
					{
						Id = album3.Id,
						Title = album3.Title,
						PhotoModels = null
					}
				}
			},
			new()
			{
				UserId = 2,
				AlbumModels = new List<AlbumModel>
				{
					new()
					{
						Id = album4.Id,
						Title = album4.Title,
						PhotoModels = new List<PhotoModel>
						{
							new()
							{
								Id = photo4.Id,
								ThumbnailUrl = photo4.ThumbnailUrl,
								Title = photo4.Title,
								Url = photo4.Url
							}
						}
					},
					new()
					{
						Id = album5.Id,
						Title = album5.Title,
						PhotoModels = new List<PhotoModel>
						{
							new()
							{
								Id = photo5.Id,
								ThumbnailUrl = photo5.ThumbnailUrl,
								Title = photo5.Title,
								Url = photo5.Url
							}
						}
					}
				}
			}
		};
		var expectedResult = userId != default ? fullResult.Where(x=>x.UserId == userId).ToList() : fullResult;
		
		//Act
		var result = _sut.Aggregate(userId, stubPhotoList, stubAlbumList);
		
		//Assert
		Assert.NotNull(result);
		Assert.Equal(expectedResult.Count, result.Count);
		for (var i = 0; i < result.Count; i++)
		{
			Assert.Equal(expectedResult[i].UserId, result[i].UserId);
			
			for (var j = 0; j < result[i].AlbumModels.Count; j++)
			{
				Assert.Equal(expectedResult[i].AlbumModels[j].Id, result[i].AlbumModels[j].Id);
				Assert.Equal(expectedResult[i].AlbumModels[j].Title, result[i].AlbumModels[j].Title);

				if (result[i].AlbumModels[j].PhotoModels == null) continue;

				for (var k = 0; k < result[i].AlbumModels[j].PhotoModels.Count; k++)
				{
					Assert.Equal(expectedResult[i].AlbumModels[j].PhotoModels[k].Id, result[i].AlbumModels[j].PhotoModels[k].Id);
					Assert.Equal(expectedResult[i].AlbumModels[j].PhotoModels[k].Title, result[i].AlbumModels[j].PhotoModels[k].Title);
					Assert.Equal(expectedResult[i].AlbumModels[j].PhotoModels[k].ThumbnailUrl, result[i].AlbumModels[j].PhotoModels[k].ThumbnailUrl);
					Assert.Equal(expectedResult[i].AlbumModels[j].PhotoModels[k].Url, result[i].AlbumModels[j].PhotoModels[k].Url);
				}
			}
		}
	}
}