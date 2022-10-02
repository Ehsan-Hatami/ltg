using LiveTixGroup.Models;

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

	public async Task<IList<PhotoAlbumAggregatedModel>> Get(int userId)
	{
		var photos = await _httpCallHandler.GetMetaData<PhotoResponse>("photos");
		var albums = await _httpCallHandler.GetMetaData<AlbumResponse>("albums");

		return _aggregator.Aggregate(userId, photos, albums);
	}
}