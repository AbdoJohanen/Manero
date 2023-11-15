using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;
using Manero.Models.Identity;

namespace Manero.Helpers.Services.DataServices;

public class ReviewService
{   
    private readonly ReviewRepository _reviewRepository;

    public ReviewService(ReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ReviewModel> CreateReviewAsync(ReviewModel review)
    {
        if (review != null)
            return await _reviewRepository.AddAsync(review);

        return null!;
    }

    public async Task<ReviewModel> CreateReviewWithSignedInUserAsync(ReviewModel review, AppUser user)
    {
        if (review != null)
        {
            if (user != null)
            {
                review.Reviewer = user.Name;
                review.ImageUrl = user.ImageUrl;
            }

            return await _reviewRepository.AddAsync(review);
        }

        return null!;
    }

    public async Task<IEnumerable<ReviewModel>> GetAllProductReviewsAsync(string articleNumber)
    {
        var reviews = new List<ReviewModel>();
        foreach (var review in await _reviewRepository.GetAllAsync(x => x.ArticleNumber == articleNumber))
            reviews.Add(review);

        return reviews;
    }
}
