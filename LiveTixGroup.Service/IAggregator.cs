using LiveTixGroup.Models;
using LiveTixGroup.Models.ExternalApiResponses;

namespace LiveTixGroup.Service;

public interface IAggregator
{
	/// <summary>
	/// Aggregates the photos and albums list according to their entity relationship.
	/// It also can filter result by the given userId
	/// </summary>
	/// <param name="userId"></param>
	/// <param name="photos"></param>
	/// <param name="albums"></param>
	/// <returns></returns>
	IList<PhotoAlbumAggregatedModel> Aggregate(int userId, IList<PhotoApiResponse> photos, IList<AlbumApiResponse> albums);
}