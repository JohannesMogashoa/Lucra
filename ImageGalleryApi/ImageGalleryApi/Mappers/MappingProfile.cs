using AutoMapper;
using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs.Auth;
using ImageGalleryApi.Models.DTOs.Images;

namespace ImageGalleryApi.Mappers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Image, ImageDto>();
		CreateMap<User, UserDto>();
	}
}