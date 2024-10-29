using AutoMapper;
using DogReviewApp.Data;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;

namespace DogReviewApp.Repository
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly DataContext _context;

		public ReviewRepository(DataContext context)
		{
			_context = context;

		}
		public Review GetReview(int ReviewId)
		{
			return _context.Reviews.Where(r => r.Id == ReviewId).FirstOrDefault();
		}

		public ICollection<Review> GetReviewOfADog(int DogId)
		{
			return _context.Reviews.Where(d => d.Dog.Id == DogId).ToList();
		}

		public ICollection<Review> GetReviews()
		{
			return _context.Reviews.ToList();
		}

		public bool ReviewExist(int ReviewId)
		{
			return _context.Reviews.Any(r  => r.Id == ReviewId);
		}
	}
}