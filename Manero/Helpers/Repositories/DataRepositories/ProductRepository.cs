using LinqKit;
using Manero.Contexts;
using Manero.Models.Entities.ProductEntities;
using Manero.ViewModels;
using Microsoft.EntityFrameworkCore;

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
            var colorPredicate = PredicateBuilder.New<ProductEntity>(false);
            foreach (var color in filters.Colors)
            {
                colorPredicate = colorPredicate.Or(p => p.ProductColors.Any(pc => pc.Color.Color == color));
            }
            query = query.Where(colorPredicate);
        }

        // Filter by sizes
        if (filters.Sizes != null && filters.Sizes.Any())
        {
            var sizePredicate = PredicateBuilder.New<ProductEntity>(false);
            foreach (var size in filters.Sizes)
            {
                sizePredicate = sizePredicate.Or(p => p.ProductSizes.Any(ps => ps.Size.Size == size));
            }
            query = query.Where(sizePredicate);
        }

        // Filter by tags
        if (filters.Tags != null && filters.Tags.Any())
        {
            var tagsPredicate = PredicateBuilder.New<ProductEntity>(false);
            foreach (var tagName in filters.Tags)
            {
                tagsPredicate = tagsPredicate.Or(p => p.ProductTags.Any(pt => pt.Tag.Tag == tagName));
            }
            query = query.Where(tagsPredicate);
        }

        // Filter by categories
        if (filters.Categories != null && filters.Categories.Any())
        {
            var categoryPredicate = PredicateBuilder.New<ProductEntity>(false);
            foreach (var categoryName in filters.Categories)
            {
                categoryPredicate = categoryPredicate.Or(p => p.ProductCategories.Any(pc => pc.Category.Category == categoryName));
            }
            query = query.Where(categoryPredicate);
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
