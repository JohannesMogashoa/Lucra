using AutoMapper;
using ImageGalleryApi.Data;
using ImageGalleryApi.Interfaces;
using ImageGalleryApi.Models;
using ImageGalleryApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ImageGalleryApi.Services;

public class ImageService(ApplicationDbContext context, IMapper mapper) : IImageService
{
	public async Task<ApiResult<IEnumerable<ImageDto>>> GetAllImages()
	{
		var images = await context.Images.ToListAsync();

		return await ApiResult<IEnumerable<ImageDto>>.SuccessAsync(mapper.Map<IEnumerable<ImageDto>>(images));
	}

	public async Task<ApiResult<ImageDto>> GetImage(string id)
	{
		var exists = await context.Images.FirstOrDefaultAsync(i => i.Id.Equals(id));

		if (exists is null)
			return await ApiResult<ImageDto>.FailureAsync(new[] { $"Image with this ID: {id} was not found" });

		return await ApiResult<ImageDto>.SuccessAsync(mapper.Map<ImageDto>(exists));
	}

	public async Task<ApiResult<ImageDto>> UpdateImage(string id, EditImageDto image)
	{
		var exists = await context.Images.FirstOrDefaultAsync(i => i.Id.Equals(id));

		if(exists is null) return await ApiResult<ImageDto>.FailureAsync(new[] { $"Image with this ID: {id} was not found" });

		exists.Description = image.Description;
		exists.Title = image.Title;
		exists.DateModified = DateTime.Now;

		try
		{
			context.Update(exists);
			if (await context.SaveChangesAsync() > 0)
			{
				return await ApiResult<ImageDto>.SuccessAsync(mapper.Map<ImageDto>(exists));
			}

			return await ApiResult<ImageDto>.FailureAsync(new[] { "Something went wrong trying to save this image" });
		}
		catch (Exception e)
		{
			return await ApiResult<ImageDto>.FailureAsync(new[] { e.Message });
		}
	}

	public async Task<ApiResult<ImageDto>> CreateImage(CreateImageDto image, string baseUri)
	{
		var createdImage = new Image
		{
			Title = image.Title,
			Description = image.Description,
			DateModified = DateTime.Now,
			CreatedOn = DateTime.Now
		};

		var fileName = await SaveFileAsync(image.Data, createdImage.Id);

		createdImage.ImageUrl = $"{baseUri}/protected/gallery/{fileName}";

		try
		{
			var entityEntry = await context.Images.AddAsync(createdImage);

			if (await context.SaveChangesAsync() > 0)
			{
				return await ApiResult<ImageDto>.SuccessAsync(mapper.Map<ImageDto>(entityEntry.Entity));
			}

			return await ApiResult<ImageDto>.FailureAsync(new[] { "Something went wrong trying to save this image" });
		}
		catch (Exception e)
		{
			return await ApiResult<ImageDto>.FailureAsync(new[] { e.Message });
		}
	}

	public async Task<ApiResult<string>> DeleteImage(string id)
	{
		var exists = await context.Images.FirstOrDefaultAsync(i => i.Id.Equals(id));
		if(exists is null) return await ApiResult<string>.FailureAsync(new[] { $"Image with this ID: {id} was not found" });

		try
		{
			var entityEntry = context.Remove(exists);

			if (await context.SaveChangesAsync() > 0)
			{
				return await ApiResult<string>.SuccessAsync(
					$"Image with ID: {entityEntry.Entity.Id} was deleted successfully!");
			}

			return await ApiResult<string>.FailureAsync(new[] { "Something went wrong trying to delete this image" });
		}
		catch (Exception e)
		{
			return await ApiResult<string>.FailureAsync(new[] { e.Message });
		}
	}


	private static async Task<string> SaveFileAsync(IFormFile file, string imageId)
	{
		const string dir = $"wwwroot/protected/gallery/";

		var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), dir);

		if (!Directory.Exists(uploadsFolder))
			Directory.CreateDirectory(uploadsFolder);

		var uniqueFileName = $"{imageId}_{file.FileName}";

		var filePath = Path.Combine(uploadsFolder, uniqueFileName);

		await using var stream = new FileStream(filePath, FileMode.Create);
		await file.CopyToAsync(stream);

		return uniqueFileName;
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