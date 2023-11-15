using Manero.Helpers.Services.DataServices;
using Manero.Helpers.Services.UserServices;
using Manero.Models.DTO;
using Manero.Models.Identity;
using Manero.ViewModels.Reviews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Manero.Controllers;

public class ReviewsController : Controller
{
    private readonly ReviewService _reviewService;
    private readonly ProductService _productService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserService _userService;

    public ReviewsController(ReviewService reviewService, ProductService productService, SignInManager<AppUser> signInManager, UserService userService)
    {
        _reviewService = reviewService;
        _productService = productService;
        _signInManager = signInManager;
        _userService = userService;
    }

    public async Task<IActionResult> Index(string articleNumber)
    {
        var viewModel = new ProductReviewsSectionViewModel();
        viewModel.Product = await _productService.GetProductAsync(articleNumber);
        
        foreach (var review in await _reviewService.GetAllProductReviewsAsync(viewModel.Product.ArticleNumber))
            viewModel.Product.Reviews.Add(review);

        return View(viewModel);
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

        if (!_signInManager.IsSignedIn(User))
        {
            if (await _reviewService.CreateReviewAsync(viewModel) != null)
                return RedirectToAction("Index", new { articleNumber = viewModel.ArticleNumber });
        }
        else
        {
            var user = await _userService.GetUserAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            if (await _reviewService.CreateReviewWithSignedInUserAsync(viewModel, user) != null)
                return RedirectToAction("Index", new { articleNumber = viewModel.ArticleNumber });
        }

        ModelState.AddModelError("", "Something went wrong, could not create review");
        return View(viewModel);
    }
}
