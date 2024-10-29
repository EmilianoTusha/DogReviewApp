using AutoMapper.Configuration.Conventions;
using DogReviewApp.Model;

namespace DogReviewApp.Interfaces
{
	public interface IReviewerRepository
	{
		ICollection<Reviewer> GetReviewers();
		Reviewer GetReviewer(int reviewerId);
		bool ExistReviewer(int reviewerId);
		ICollection<Review> GetReviewByReviewers(int reviewerId);
	}
}
