using Manero.Helpers.Repositories.DataRepositories;

namespace Manero.Helpers.Services.DataServices;

public class ReviewService
{   
    private readonly ReviewRepository _reviewRepository;

    public ReviewService(ReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
}
