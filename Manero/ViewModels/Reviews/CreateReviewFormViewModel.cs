using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Manero.Models.DTO;

namespace Manero.ViewModels.Reviews;

public class CreateReviewFormViewModel
{
    [DisplayName("NAME")]
    public string? Reviewer { get; set; }

    [DisplayName("COMMENT")]
    [Required]
    public string Comment { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int Rating { get; set; }

    [HiddenInput]
    public string ArticleNumber { get; set; } = null!;

    public static implicit operator ReviewModel(CreateReviewFormViewModel viewModel)
    {
        return new ReviewModel
        {
            Reviewer = viewModel.Reviewer,
            Comment = viewModel.Comment,
            Rating = viewModel.Rating,
            ArticleNumber = viewModel.ArticleNumber,
        };
    }
}
