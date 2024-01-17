using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGalleryApi.Models.DTOs;

public class CreateImageDto
{
	[Required]
	[JsonProperty("title")]
	public required string Title { get; set; }

	[Required]
	[JsonProperty("description")]
	public required string Description { get; set; }

	[JsonProperty("data")]
	public IFormFile Data { get; set; }
}