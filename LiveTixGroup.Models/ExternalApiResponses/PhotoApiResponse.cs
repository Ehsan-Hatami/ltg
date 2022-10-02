using System.Text.Json.Serialization;

namespace LiveTixGroup.Models.ExternalApiResponses;

public class PhotoApiResponse
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("albumId")]
	public int AlbumId { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; }

	[JsonPropertyName("url")]
	public string Url { get; set; }

	[JsonPropertyName("thumbnailUrl")]
	public string ThumbnailUrl { get; set; }
}