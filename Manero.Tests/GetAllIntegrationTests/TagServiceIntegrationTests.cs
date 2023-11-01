using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero.Tests.GetAllIntegrationTests
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
        public async Task GetAllCategoriesToModelAsync_ShouldReturnCategoriesInModelFormat()
        {
            // Arrange: Lägg till testdata i din InMemory-databas
            var tag1 = new TagModel { Id = 1, Tag = "Tag 1" };
            var tag2 = new TagModel { Id = 2, Tag = "Tag 2" };

            await _context.Tags.AddRangeAsync(tag1, tag2);
            await _context.SaveChangesAsync();

            // Act: Anropa GetAllTagsToModelAsync-metoden
            var result = await _service.GetAllTagsToModelAsync();

            // Assert: Kontrollera att resultatet är en lista med tagmodeller i rätt format
            Assert.NotNull(result);
            Assert.IsType<List<TagModel>>(result);

            // Kontrollera att antalet tagmodeller matchar antalet kategorier i databasen
            Assert.Equal(2, result.Count());

            // Rensa InMemory-databasen efter testet om det behövs
        }
    }

}
