namespace ImageGalleryApi.Interfaces;

public interface IApiResult
{
	string[] Errors { get; init; }

	bool Succeeded { get; init; }
}

public interface IApiResult<out T> : IApiResult
{
	T? Data { get; }
}