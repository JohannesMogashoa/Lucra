using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageGalleryApi.Models;

public class User
{
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public string Id { get; set; } = $"{Guid.NewGuid()}";

	[Required]
	public required string UserName { get; set; }

	[Required]
	public required string FullName { get; set; }

	[Required]
	public required string PasswordHash { get; set; }

	public List<Image>? Images { get; set; } = [];
}