using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGalleryApi.Models.DTOs.Auth;

public class LoginUserDto
{
	[Required]
	[JsonProperty("username")]
	public required string UserName { get; set; }

	[Required]
	[JsonProperty("password")]
	[DataType(DataType.Password)]
	public required string Password { get; set; }
}