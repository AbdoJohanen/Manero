using Manero.Helpers.Services.DataServices;
using Manero.ViewModels.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace Manero.Controllers;

public class ReviewsController : Controller
{
    private ReviewService _reviewService;

    public ReviewsController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public IActionResult Reviews()
    {
        return View();
    }

    [HttpGet]
    public IActionResult CreateReview(string articleNumber)
    {
        var viewModel = new CreateReviewFormViewModel();
        viewModel.ArticleNumber = articleNumber;
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Comment and Rating are required");
            return View(viewModel);
        }

        if (await _reviewService.CreateReviewAsync(viewModel) != null)
            return RedirectToAction("Reviews");

        ModelState.AddModelError("", "Something went wrong, could not create review");
        return View(viewModel);
    }
}
