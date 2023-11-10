using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Manero.Tests.GeorgeTests.GetAllIntegrationTests
{
    public class TagServiceIntegrationTests
    {
        private readonly TagService _service;
        private readonly TagRepository _repository;
        private readonly DataContext _context;

        public TagServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new DataContext(options);
            _repository = new TagRepository(_context);
            _service = new TagService(_repository);
        }

        [Fact]
        public async Task GetAllTagsToModelAsync_ShouldReturnTagsInModelFormat()
        {
            // Arrange
            var tag1 = new TagModel { Id = 1, Tag = "Tag 1" };
            var tag2 = new TagModel { Id = 2, Tag = "Tag 2" };

            await _context.Tags.AddRangeAsync(tag1, tag2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllTagsToModelAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TagModel>>(result);

            // Kontrollera att antalet tagmodeller matchar antalet Taggar i databasen
            Assert.Equal(2, result.Count());

        }
    }

}
