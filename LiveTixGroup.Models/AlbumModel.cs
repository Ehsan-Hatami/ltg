namespace LiveTixGroup.Models;

public class AlbumModel
{
	public int Id { get; set; }
	public string Title { get; set; }
	public List<PhotoModel> PhotoModels { get; set; }
}