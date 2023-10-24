using Manero.Models.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace Manero.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected DataContext()
    {
    }


    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<ProductTagEntity> ProductTags { get; set; }
    public DbSet<ColorEntity> Colors { get; set; }
    public DbSet<ProductColorEntity> ProductColors { get; set; }
    public DbSet<SizeEntity> Sizes { get; set; }
    public DbSet<ProductSizeEntity> ProductSizes { get; set; }
    public DbSet<ImageEntity> Images { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TagEntity>().HasData(
            new TagEntity { Id = 1, Tag = "Featured Products" },
            new TagEntity { Id = 2, Tag = "Best Sellers" },
            new TagEntity { Id = 3, Tag = "Sale" },
            new TagEntity { Id = 4, Tag = "New" }
        );

        modelBuilder.Entity<CategoryEntity>().HasData(
            new CategoryEntity { Id = 1, Category = "Dresses"},
            new CategoryEntity { Id = 2, Category = "Pants" },
            new CategoryEntity { Id = 3, Category = "Accessories" },
            new CategoryEntity { Id = 4, Category = "Shoes" },
            new CategoryEntity { Id = 5, Category = "T-shirts" }

            );
    }
}
