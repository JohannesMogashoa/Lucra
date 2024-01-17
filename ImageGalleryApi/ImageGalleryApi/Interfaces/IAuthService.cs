using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs.Auth;

namespace ImageGalleryApi.Interfaces;

public interface IAuthService
{
	Task<ApiResult<bool>> RegisterUser(RegisterUserDto user);
	Task<ApiResult<UserDto>> LoginUser(LoginUserDto user);
}