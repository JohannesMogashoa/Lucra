using Newtonsoft.Json;

namespace ImageGalleryApi.Models.DTOs.Images;

public class ImageDto
{
	[JsonProperty("id")]
	public required string Id { get; set; }

	[JsonProperty("title")]
	public required string Title { get; set; }

	[JsonProperty("image_url")]
	public required string ImageUrl { get; set; }

	[JsonProperty("description")]
	public required string Description { get; set; }

	[JsonProperty("date_modified")]
	public DateTime DateModified { get; set; }

	[JsonProperty("created_on")]
	public DateTime CreatedOn { get; set; }
}