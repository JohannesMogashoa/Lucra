using ImageGalleryApi.Interfaces;
using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController(IImageService imageService) : ControllerBase
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
	[ProducesResponseType(typeof(ApiResult<IEnumerable<ImageDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAllImages()
	{
		var result = await imageService.GetAllImages();
		return Ok(result);
	}

	/// <summary>
	/// Retrieve single Image
	/// </summary>
	/// <response code="200">Returns Image</response>
	/// <response code="401">User is unauthorizeed</response>
	/// <response code="404">User credentials could not be found</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns Image</returns>
	[HttpGet("{id}", Name = "Get Image")]
	[ProducesResponseType(typeof(ApiResult<ImageDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetImage(string id)
	{
		var result = await imageService.GetImage(id);

		if (result.Succeeded) return Ok(result);

		return BadRequest(result);
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
	public async Task<IActionResult> CreateImage([FromForm] CreateImageDto model)
	{
		if (!ModelState.IsValid) return BadRequest("Model State is invalid");

		var baseUri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

		var result = await imageService.CreateImage(model, baseUri);

		if (result.Succeeded) return Created(result.Data?.ImageUrl, result);

		return BadRequest(result);
	}

	/// <summary>
	/// Updates a image information
	/// </summary>
	/// <response code="200">Returns Image</response>
	/// <response code="401">User is unauthorizeed</response>
	/// <response code="404">User credentials could not be found</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns Image</returns>
	[HttpPut("{id}", Name = "Update Image")]
	[ProducesResponseType(typeof(ImageDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> UpdateImage([FromRoute] string id, [FromBody] EditImageDto model)
	{
		if (!ModelState.IsValid) return BadRequest("The modelstate is not valid");

		var result = await imageService.UpdateImage(id, model);

		if (result.Succeeded) return Ok(result);

		return BadRequest(result);

	}

	/// <summary>
	/// Removes an image
	/// </summary>
	/// <response code="200">Returns image id</response>
	/// <response code="401">User is unauthorizeed</response>
	/// <response code="404">User credentials could not be found</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns id</returns>
	[HttpDelete("{id}", Name = "Delete Image")]
	[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteImage([FromRoute] string id)
	{
		var result = await imageService.DeleteImage(id);

		if (result.Succeeded) return Ok(result);

		return BadRequest(result);
	}
}