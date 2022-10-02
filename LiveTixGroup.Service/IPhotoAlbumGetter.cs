using LiveTixGroup.Models;

namespace LiveTixGroup.Service;

public interface IPhotoAlbumGetter
{
	/// <summary>
	/// Gets the aggregated result for photos and albums
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	Task<IList<PhotoAlbumAggregatedModel>> Get(int userId);
}