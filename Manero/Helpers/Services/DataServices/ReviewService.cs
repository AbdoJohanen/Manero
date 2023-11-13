using Manero.Helpers.Repositories.DataRepositories;
using Manero.Models.DTO;

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
}
