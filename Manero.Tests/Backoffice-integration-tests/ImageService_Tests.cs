using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Text;
using Xunit;

namespace Manero.Tests.Backoffice_integration_tests;

public class ImageService_Tests : IDisposable
{
    private readonly ImageService _service;
    private readonly ImageRepository _repository;
    private readonly DataContext _context;
    private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;

    public ImageService_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        _context = new DataContext(options);
        _repository = new ImageRepository(_context);

        _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);
        Directory.CreateDirectory(Path.Combine(tempPath, "assets", "images", "products"));
        _mockWebHostEnvironment.Setup(x => x.WebRootPath).Returns(tempPath); // Modify as necessary for your test.

        _service = new ImageService(_mockWebHostEnvironment.Object, _repository);
    }

    [Fact]
    public async Task SaveProductImageAsync_Should_ReturnImageModel_When_SavedSuccessfully()
    {
        // Arrange
        var product = new ProductModel() { ArticleNumber = "Test-1" };
        // Mock IFormFile
        var mockFormFile = new Mock<IFormFile>();
        var sourceImg = "Hello World from a fake file";
        var content = new MemoryStream(Encoding.UTF8.GetBytes(sourceImg));
        var fileName = "test.jpg";
        mockFormFile.Setup(f => f.FileName).Returns(fileName);
        mockFormFile.Setup(f => f.Length).Returns(content.Length);
        mockFormFile.Setup(m => m.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None))
                    .Returns((Stream stream, CancellationToken token) => content.CopyToAsync(stream));

        // Act
        var result = await _service.SaveProductImageAsync(product, mockFormFile.Object, true);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ImageModel>(result);
    }

    public void Dispose()
    {
        try
        {

            var tempPath = _mockWebHostEnvironment.Object.WebRootPath;
            DeleteDirectory(tempPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cleanup error: " + ex.Message);
        }
    }

    private void DeleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
            }

            foreach (string dir in Directory.GetDirectories(path))
            {
                DeleteDirectory(dir);
            }

            try
            {
                Directory.Delete(path, recursive: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting directory {path}: {ex.Message}");
            }
        }
    }
}