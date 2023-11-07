using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace Manero.Tests;

public class GetProductNameTests








{
    private readonly ProductService _service;
    private readonly ProductRepository _repository;
    private readonly DataContext _context;

    public GetProductNameTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);
        _repository = new ProductRepository(_context);
        _service = new ProductService(_repository);


    }

    [Fact]
    public async Task GetProductWithImagesAsync_ShouldReturnProductName()
    {
        var product = new ProductModel { ArticleNumber = "1", ProductName = "Product 1" };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var result = await _service.GetProductWithImagesAsync(product.ArticleNumber);

        Assert.Equal("Product 1", result.ProductName);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}




/*{
    private readonly ProductService _productService;

    [Fact]
    public async Task GetProductName_From_ProductDB()
    {
        //Arrange
        var expectedProductName = new ProductModel
        {
            ArticleNumber = "1",
            ProductName = "Test Product"

        };

        await _productService.CreateProductAsync(expectedProductName);


        //Act
        var result = await _productService.GetProductWithImagesAsync(expectedProductName.ArticleNumber);


        //Assert
        Assert.Equal("Test Product", result.ProductName);

    }


}*/
