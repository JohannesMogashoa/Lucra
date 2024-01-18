using System.Reflection;
using System.Text;
using System.Text.Json;
using ImageGalleryApi.Configurations;
using ImageGalleryApi.Data;
using ImageGalleryApi.Interfaces;
using ImageGalleryApi.Mappers;
using ImageGalleryApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", OpenAPIConfig.GetApiInfo());
    opt.AddSecurityDefinition("Bearer", OpenAPIConfig.GetSecurityScheme());
    opt.AddSecurityRequirement(OpenAPIConfig.GetSecurityRequirement());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7210",
        ValidAudience = "https://localhost:7210",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9eb16bf2-a3f2-4b89-84ed-04456943c1c2-4efabca0-5f2e-472e-9dfc-fe94589b3a3d")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };
});

builder.Services.AddCors();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(options => options.AllowAnyOrigin());

app.MapControllers();

app.Run();