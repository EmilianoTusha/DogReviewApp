using DogReviewApp.Model;

namespace DogReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		ICollection<Review> GetReviews();
		Review GetReview(int ReviewId);
		ICollection<Review> GetReviewOfADog(int DogId);
		bool ReviewExist(int ReviewId);

	}
}
