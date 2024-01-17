using ImageGalleryApi.Interfaces;
using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalleryApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IAuthService authService) : ControllerBase
{
	/// <summary>
	/// Register new user
	/// </summary>
	/// <response code="200">Returns boolean</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns boolean</returns>
	[HttpPost("register", Name = "Register User")]
	[ProducesResponseType(typeof(ApiResult<bool>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
	{
		if (!ModelState.IsValid) return BadRequest("Modelstate is invalid");

		var result = await authService.RegisterUser(registerUserDto);

		if (result.Succeeded) return Ok(result);

		return BadRequest(result);
	}

	/// <summary>
	/// Authenticate user
	/// </summary>
	/// <response code="200">Returns User</response>
	/// <response code="500">Internal Server Error - Something went wrong</response>
	/// <returns>Returns User</returns>
	[HttpPost("login", Name = "Authenticate User")]
	[ProducesResponseType(typeof(ApiResult<UserDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> AuthenticateUser([FromBody] LoginUserDto loginUserDto)
	{
		if (!ModelState.IsValid) return BadRequest("Model State is invalid");

		var result = await authService.LoginUser(loginUserDto);

		if (result.Succeeded) return Ok(result);

		return Unauthorized(result);
	}
}