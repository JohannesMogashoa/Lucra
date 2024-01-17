using ImageGalleryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageGalleryApi.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Image> Images { get; set; }
	public DbSet<User> Users { get; set; }
}