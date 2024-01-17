using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGalleryApi.Models.DTOs.Images;

public class EditImageDto
{
	[Required]
	[JsonProperty("title")]
	public required string Title { get; set; }

	[Required]
	[JsonProperty("description")]
	public required string Description { get; set; }
}