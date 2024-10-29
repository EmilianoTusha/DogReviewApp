using AutoMapper;
using DogReviewApp.Dto;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewApp.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[Controller]")]
	[ApiController]
	public class ReviewController : Controller
	{
		

			private readonly IReviewRepository _reviewRepository;
			private readonly IMapper _mapper;
			public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
			{
				_reviewRepository = reviewRepository;
				_mapper = mapper;
			}

			[HttpGet]
			[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
			public IActionResult GetReviews()
			{
				var country = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				return Ok(country);
			}

			[HttpGet("{reviewId}")]
			[ProducesResponseType(200, Type = typeof(Review))]
			[ProducesResponseType(400)]
		public IActionResult GetReview(int reviewId)
		{
			if (!_reviewRepository.ReviewExist(reviewId))
				return NotFound();

			var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(review);
		}

		[HttpGet("dog/{dogId}")]
		[ProducesResponseType(200, Type = typeof(Review))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewsForAdog(int dogId)
		{
			var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewOfADog(dogId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(reviews);
		}


	}

}
