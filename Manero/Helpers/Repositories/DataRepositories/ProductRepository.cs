using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;
using Manero.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Manero.Helpers.Repositories.DataRepositories;

public class ProductRepository : DataRepository<ProductEntity>
{
    public ProductRepository(DataContext context) : base(context)
    {

    }

    // Add a method to filter products based on the provided ViewModel
    public async Task<List<ProductEntity>> GetFilteredProductsAsync(ProductFilterViewModel filters)
    {
        IQueryable<ProductEntity> query = _context.Products;

        // Filter by colors
        if (filters.Colors != null && filters.Colors.Any())
        {
            foreach (var color in filters.Colors)
            {
                query = query.Where(p => p.ProductColors.Any(pc => pc.Color.Color == color));
            }
        }

        // Filter by sizes
        if (filters.Sizes != null && filters.Sizes.Any())
        {
            foreach (var size in filters.Sizes)
            {
                query = query.Where(p => p.ProductSizes.Any(ps => ps.Size.Size == size));
            }
        }

        // Filter by tags
        if (filters.Tags != null && filters.Tags.Any())
        {
            foreach (var tagName in filters.Tags)
            {
                query = query.Where(p => p.ProductTags.Any(pt => pt.Tag.Tag == tagName));
            }
        }

        // Filter by categories
        if (filters.Categories != null && filters.Categories.Any())
        {
            foreach (var categoryName in filters.Categories)
            {
                query = query.Where(p => p.ProductCategories.Any(pc => pc.Category.Category == categoryName));
            }
        }

        // Filter by minimum price if specified
        if (filters.MinPrice.HasValue)
        {
            query = query.Where(p => p.ProductPrice >= filters.MinPrice.Value);
        }

        // Filter by maximum price if specified
        if (filters.MaxPrice.HasValue)
        {
            query = query.Where(p => p.ProductPrice <= filters.MaxPrice.Value);
        }

        return await query.Distinct().AsNoTracking().ToListAsync();
    }

}
