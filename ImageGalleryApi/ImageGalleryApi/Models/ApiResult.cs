using ImageGalleryApi.Interfaces;

namespace ImageGalleryApi.Models;

public class ApiResult : IApiResult
{
	internal ApiResult()
	{
		Errors = [];
	}

	private ApiResult(bool succeeded, IEnumerable<string> errors)
	{
		Succeeded = succeeded;
		Errors = errors.ToArray();
	}

	public bool Succeeded { get; init; }

	public string[] Errors { get; init; }

	public string ErrorMessage => string.Join(", ", Errors ?? new string[] { });

	public static ApiResult Success()
	{
		return new ApiResult(true, Array.Empty<string>());
	}
	public static Task<ApiResult> SuccessAsync()
	{
		return Task.FromResult(new ApiResult(true, Array.Empty<string>()));
	}
	public static ApiResult Failure(IEnumerable<string> errors)
	{
		return new ApiResult(false, errors);
	}
	public static Task<ApiResult> FailureAsync(IEnumerable<string> errors)
	{
		return Task.FromResult(new ApiResult(false, errors));
	}
}
public class ApiResult<T> : ApiResult, IApiResult<T>
{

	public T? Data { get; private init; }

	private new static ApiResult<T> Failure(IEnumerable<string> errors)
	{
		return new ApiResult<T> { Succeeded = false, Errors = errors.ToArray() };
	}
	public new static async Task<ApiResult<T>> FailureAsync(IEnumerable<string> errors)
	{
		return await Task.FromResult(Failure(errors));
	}

	private static ApiResult<T> Success(T data)
	{
		return new ApiResult<T> { Succeeded = true, Data = data };
	}
	public static async Task<ApiResult<T>> SuccessAsync(T data)
	{
		return await Task.FromResult(Success(data));
	}
}