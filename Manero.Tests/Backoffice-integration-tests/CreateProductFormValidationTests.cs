using Manero.ViewModels.BackOffice;
using Microsoft.AspNetCore.Http;
using Moq;
using System.ComponentModel.DataAnnotations;


namespace Manero.Tests.Backoffice_integration_tests;

public class CreateProductFormValidationTests
{

    [Fact]
    public void Validation_Should_Fail_When_Required_Fields_Are_Missing()
    {
        // Arrange
        var viewModel = new CreateProductFormViewModel();

        // Act
        var validationContext = new ValidationContext(viewModel, null, null);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(viewModel, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);

        // Verify that errors are present for required fields
        Assert.NotEmpty(validationResults);
    }


    [Fact]
    public void Validation_Should_PassWhenAllRequiredFieldsAreFilled()
    {
        // Arrange
        var viewModel = new CreateProductFormViewModel
        {
            ArticleNumber = "ABC123",
            ProductName = "Test Product 1",
            ProductPrice = 10.0M,
            SelectedTags = new List<int> { 1 },
            SelectedCategories = new List<int> { 1 },
            Images = new List<IFormFile> { CreateMockImageFile("sample.jpg") },
            SelectedSizes = new List<int> { 1 },
            SelectedColors = new List<int> { 1 }
        };

        // Act
        var validationContext = new ValidationContext(viewModel, null, null);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(viewModel, validationContext, validationResults, true);

        // Assert
        Assert.True(isValid);
    }

    private IFormFile CreateMockImageFile(string fileName)
    {
        var mock = new Mock<IFormFile>();
        mock.Setup(f => f.FileName).Returns(fileName);
        return mock.Object;
    }

}
