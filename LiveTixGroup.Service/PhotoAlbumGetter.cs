using LiveTixGroup.Models;
using LiveTixGroup.Models.ExternalApiResponses;

namespace LiveTixGroup.Service;

public class PhotoAlbumGetter : IPhotoAlbumGetter
{
	private readonly IHttpCallHandler _httpCallHandler;
	private readonly IAggregator _aggregator;

	public PhotoAlbumGetter(IHttpCallHandler httpCallHandler, IAggregator aggregator)
	{
		_httpCallHandler = httpCallHandler;
		_aggregator = aggregator;
	}

	/// <inheritdoc />
	public async Task<IList<PhotoAlbumAggregatedModel>> Get(int userId)
	{
		var photos = await _httpCallHandler.GetMetaData<PhotoApiResponse>("photos");
		var albums = await _httpCallHandler.GetMetaData<AlbumApiResponse>("albums");

		return _aggregator.Aggregate(userId, photos, albums);
	}
}