using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ImageGalleryApi.Data;
using ImageGalleryApi.Interfaces;
using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs.Auth;
using Microsoft.IdentityModel.Tokens;

namespace ImageGalleryApi.Services;

public class AuthService(ApplicationDbContext context, IMapper mapper) : IAuthService
{
	public async Task<ApiResult<bool>> RegisterUser(RegisterUserDto user)
	{

		if (context.Users.Any(u => u.UserName == user.UserName))
		{
			return await ApiResult<bool>.FailureAsync(new [] {$"Username, {user.UserName} already exists"});
		}

		// Hash the password (in a production scenario, use a more secure hashing algorithm)
		var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

		var newUser = new User
		{
			FullName = user.FullName,
			UserName = user.UserName,
			PasswordHash = passwordHash
		};

		context.Users.Add(newUser);
		if (await context.SaveChangesAsync() > 0)
		{
			return await ApiResult<bool>.SuccessAsync(true);
		}

		return await ApiResult<bool>.FailureAsync(new[] { "Something went wrong registering the user." });
	}

	public async Task<ApiResult<UserDto>> LoginUser(LoginUserDto user)
	{
		var dbUser = context.Users.SingleOrDefault(u => u.UserName == user.UserName);

		if (dbUser != null && BCrypt.Net.BCrypt.Verify(user.Password, dbUser.PasswordHash))
		{
			var token = GenerateJwtToken(dbUser);
			var userDto = mapper.Map<UserDto>(dbUser);
			userDto.AuthToken = token;

			return await ApiResult<UserDto>.SuccessAsync(userDto);
		}

		return await ApiResult<UserDto>.FailureAsync(new []{"Invalid User Credentials"});
	}

	private static string GenerateJwtToken(User user)
	{
		byte[] secret = Encoding.UTF8.GetBytes("9eb16bf2-a3f2-4b89-84ed-04456943c1c2-4efabca0-5f2e-472e-9dfc-fe94589b3a3d");
		IEnumerable<System.Security.Claims.Claim> claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.UserName)
		};

		var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
		JwtSecurityToken token = new(
			claims: claims,
			expires: DateTime.UtcNow.AddDays(2),
			signingCredentials: signingCredentials);
		JwtSecurityTokenHandler tokenHandler = new();
		string? encryptedToken = tokenHandler.WriteToken(token);
		return encryptedToken;
	}
}