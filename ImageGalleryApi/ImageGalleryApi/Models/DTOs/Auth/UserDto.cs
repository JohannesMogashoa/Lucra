using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGalleryApi.Models.DTOs.Auth;

public class UserDto
{
	[Required]
	[JsonProperty("full_name")]
	public required string FullName { get; set; }

	[Required]
	[JsonProperty("username")]
	public required string UserName { get; set; }

	[Required]
	[JsonProperty("auth_token")]
	public required string AuthToken { get; set; }
}