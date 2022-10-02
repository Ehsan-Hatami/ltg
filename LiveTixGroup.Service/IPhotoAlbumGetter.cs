using LiveTixGroup.Models;

namespace LiveTixGroup.Service;

public interface IPhotoAlbumGetter
{
	Task<IList<PhotoAlbumAggregatedModel>> Get(int userId);
}