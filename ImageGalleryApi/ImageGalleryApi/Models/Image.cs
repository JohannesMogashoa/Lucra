using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageGalleryApi.Models;

public class Image
{
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	public string Id { get; set; } = $"{Guid.NewGuid()}";

	[Required]
	[StringLength(120, ErrorMessage = "The {0} must be at least {2} and at a max {1} characters long.", MinimumLength = 2)]
	public required string Title { get; set; }

	[Required]
	public string ImageUrl { get; set; }

	[Required]
	public required string Description { get; set; }

	public DateTime DateModified { get; set; }

	public DateTime CreatedOn { get; set; }

	public User? User { get; set; }
}