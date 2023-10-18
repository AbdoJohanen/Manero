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
    public DbSet<ProductImageEntity> ProductImages { get; set; }
    public DbSet<ProductReviewEntity> ProductReviews { get; set; }

}
