namespace LiveTixGroup.Models;

public class PhotoAlbumAggregatedModel
{
	public int UserId { get; set; }
	public List<AlbumModel> AlbumModels { get; set; }
}