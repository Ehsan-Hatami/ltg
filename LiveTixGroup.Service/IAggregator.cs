using LiveTixGroup.Models;

namespace LiveTixGroup.Service;

public interface IAggregator
{
	IList<PhotoAlbumAggregatedModel> Aggregate(int userId, IList<PhotoResponse> photos, IList<AlbumResponse> albums);
}