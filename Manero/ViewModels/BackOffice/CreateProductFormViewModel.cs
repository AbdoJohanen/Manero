﻿using Manero.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Manero.ViewModels.BackOffice;

public class CreateProductFormViewModel
{
    [Required(ErrorMessage = "Please enter a product article number")]
    [Display(Name = "Article number *")]
    public string ArticleNumber { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a product name")]
    [Display(Name = "Product name *")]
    public string ProductName { get; set; } = null!;

    [Display(Name = "Description *")]
    public string? ProductDescription { get; set; }

    [Required(ErrorMessage = "Please enter a price")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    [Display(Name = "Price *")]
    public decimal ProductPrice { get; set; }

    [Display(Name = "Discount Price *")]
    public decimal? ProductDiscount { get; set; }

    [Required(ErrorMessage = "Please Choose atleast one tag")]
    [Display(Name = "Tags (Choose one or more) *")]
    public List<int> SelectedTags { get; set; } = null!;
    public IEnumerable<TagModel> Tags { get; set; } = new HashSet<TagModel>();

    [Required(ErrorMessage = "Please Choose atleast one category")]
    [Display(Name = "Categories (Choose one or more) *")]
    public List<int> SelectedCategories { get; set; } = null!;
    public IEnumerable<CategoryModel> Categories { get; set; } = new HashSet<CategoryModel>();

    [Required(ErrorMessage = "Please choose atleast one image")]
    [Display(Name = "Upload Images (Optional)")]
    [DataType(DataType.Upload)]
    public List<IFormFile> Images { get; set; } = null!;

    public string MainImageFileName { get; set; } = null!;

    [Required(ErrorMessage = "Please Choose atleast one size")]
    [Display(Name = "Sizes (Choose one or more) *")]
    public List<int> SelectedSizes { get; set; } = null!;
    public IEnumerable<SizeModel> Sizes { get; set; } = new HashSet<SizeModel>();

    [Required(ErrorMessage = "Please Choose atleast one color")]
    [Display(Name = "Color (Choose one or more) *")]
    public List<int> SelectedColors { get; set; } = null!;
    public IEnumerable<ColorModel> Colors { get; set; } = new HashSet<ColorModel>();

    public static implicit operator ProductModel(CreateProductFormViewModel viewModel)
    {
        return new ProductModel
        {
            ArticleNumber = viewModel.ArticleNumber,
            ProductName = viewModel.ProductName,
            ProductDescription = viewModel.ProductDescription!,
            ProductPrice = viewModel.ProductPrice,
            ProductDiscount = viewModel.ProductDiscount!
        };
    }
}



/*
 * Old Reference
public class CreateProductViewModel
{

    [Required(ErrorMessage = "Please enter a product name")]
    [Display(Name = "Product name *")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a description")]
    [Display(Name = "Description *")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a price")]
    [Display(Name = "Price *")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Please enter a rating")]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    [Display(Name = "Rating * (Not an option in production)")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "Please enter the total number of ratings")]
    [Display(Name = "Total Ratings * (Not an option in production)")]
    public int TotalRatings { get; set; }

    [Required(ErrorMessage = "Please enter a stock amount")]
    [Display(Name = "Stock *")]
    public int StockTotal { get; set; } = 0;

    [Display(Name = "Upload Image (Optional)")]
    [DataType(DataType.Upload)]
    public IFormFile? Image { get; set; }

    [Required(ErrorMessage = "Please Choose atleast one category")]
    [Display(Name = "Categories (Choose one or more) *")]
    public List<CategoryModel> Categories { get; set; } = null!;


    public static implicit operator ProductEntity(CreateProductViewModel viewModel)
    {
        var entity = new ProductEntity
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Rating = viewModel.Rating,
            TotalRatings = viewModel.TotalRatings,
            StockTotal = viewModel.StockTotal,
        };

        if (viewModel.Image != null)
        {
            entity.ImagePath = $"{Guid.NewGuid()}-{viewModel.Image.FileName}";
        }

        return entity;
    }
}

*/