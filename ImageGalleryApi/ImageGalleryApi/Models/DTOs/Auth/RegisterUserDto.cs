using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGalleryApi.Models.DTOs.Auth;

public class RegisterUserDto
{
	[Required]
	[JsonProperty("full_name")]
	public required string FullName { get; set; }

	[Required]
	[JsonProperty("username")]
	public required string UserName { get; set; }

	[Required]
	[JsonProperty("password")]
	[DataType(DataType.Password)]
	public required string Password { get; set; }
}