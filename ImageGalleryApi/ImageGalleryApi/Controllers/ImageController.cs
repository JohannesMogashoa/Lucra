using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
	/// <summary>
	/// Retrieve all Images
	/// </summary>
	/// <response code="200">Returns all Images</response>
	/// <response code="401">User is unauthorizeed</response>
	/// <response code="404">User credentials could not be found</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns all Images</returns>
	[HttpGet(Name = "Get All Images")]
	[ProducesResponseType(typeof(List<ImageDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAllImages()
	{
		var images = new List<ImageDto>
		{
			new()
			{
				Id = Guid.NewGuid().ToString(),
				CreatedOn = DateTime.Now,
				DateModified = DateTime.Now,
				Description = "This is a test image",
				ImageUrl = "https://image.com",
				Title = "My Image"
			},
			new()
			{
				Id = Guid.NewGuid().ToString(),
				CreatedOn = DateTime.Now,
				DateModified = DateTime.Now,
				Description = "This is a test image2",
				ImageUrl = "https://image.com",
				Title = "My Image 2"
			}
		};
		return Ok(images);
	}

	[HttpGet("{id:guid}", Name = "Get Image")]
	public async Task<IActionResult> GetImage(Guid id)
	{
		return Ok("One image here");
	}

	/// <summary>
	/// Adds Image
	/// </summary>
	/// <response code="200">Returns Image</response>
	/// <response code="401">User is unauthorizeed</response>
	/// <response code="404">User credentials could not be found</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns Image</returns>
	[HttpPost(Name = "Create Image")]
	[ProducesResponseType(typeof(ImageDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> CreateImage([FromBody] CreateImageDto model)
	{
		if (ModelState.IsValid)
		{
			var image = new Image
			{
				Title = model.Title,
				Description = model.Description,
				DateModified = DateTime.Now,
				CreatedOn = DateTime.Now
			};

			if (string.IsNullOrEmpty(model.Data))
			{
				return BadRequest("Data string is empty or null.");
			}

			var imagePath = await SaveImageFromStringAsync(model.Data, model.Title);

			image.ImageUrl = imagePath;

			return Ok(image);
		}
		return BadRequest("Model State is invalid");
	}

	[HttpPut("{id:guid}", Name = "Update Image")]
	public async Task<IActionResult> UpdateImage(Guid id, EditImageDto model)
	{
		if (ModelState.IsValid)
		{

		}

		return BadRequest("The modelstate is not valid");
	}

	[HttpDelete("{id:guid}", Name = "Delete Image")]
	public async Task<IActionResult> DeleteImage(Guid id)
	{
		return Ok();
	}



	private async Task<string> SaveImageFromStringAsync(string base64String, string title)
	{
		var bytes = Convert.FromBase64String(base64String);

		var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads");
		if (!Directory.Exists(uploadsFolder))
			Directory.CreateDirectory(uploadsFolder);

		var uniqueFileName = $"{title}-{DateTime.Now}.png"; // You may want to specify the file type

		var filePath = Path.Combine(uploadsFolder, uniqueFileName);

		await System.IO.File.WriteAllBytesAsync(filePath, bytes);

		return filePath;
	}
}