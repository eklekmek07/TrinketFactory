using TrinketFactoryStore.Models;
using Microsoft.EntityFrameworkCore;

namespace TrinketFactoryStore.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, DisplayOrder = 1, Name = "Lifestyle" },
            new Category { Id = 2, DisplayOrder = 2, Name = "Sport" },
            new Category { Id = 3, DisplayOrder = 3, Name = "Health" });
    }
}