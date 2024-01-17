using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs;

namespace ImageGalleryApi.Interfaces;

public interface IImageService
{
	Task<ApiResult<IEnumerable<ImageDto>>> GetAllImages();
	Task<ApiResult<ImageDto>> GetImage(string id);
	Task<ApiResult<ImageDto>> UpdateImage(string id, EditImageDto image);
	Task<ApiResult<ImageDto>> CreateImage(CreateImageDto image, string baseUri);
	Task<ApiResult<string>> DeleteImage(string id);
}