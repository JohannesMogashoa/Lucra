using AutoMapper;
using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs;

namespace ImageGalleryApi.Mappers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Image, ImageDto>();
	}
}