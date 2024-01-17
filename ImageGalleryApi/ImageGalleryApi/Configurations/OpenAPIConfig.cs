using Microsoft.OpenApi.Models;

namespace ImageGalleryApi.Configurations;

public static class OpenAPIConfig
{
	public static OpenApiSecurityScheme GetSecurityScheme()
	{
		return new OpenApiSecurityScheme()
		{
			Name = "Authorization",
			Type = SecuritySchemeType.Http,
			Scheme = "Bearer",
			BearerFormat = "JWT",
			In = ParameterLocation.Header,
			Description = "JSON Web Token based security",
		};
	}

	public static OpenApiSecurityRequirement GetSecurityRequirement()
	{
		return new OpenApiSecurityRequirement()
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
                Array.Empty<string>()
            }
		};
	}

	public static OpenApiInfo GetApiInfo()
	{
		return new OpenApiInfo()
		{
			Version = "v1",
			Title = "Lucra Image Gallery",
			Description = "Web Api for Image Gallery Management"
		};
	}
}