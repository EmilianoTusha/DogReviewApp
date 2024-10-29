using AutoMapper;
using DogReviewApp.Dto;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;
using DogReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewApp.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[Controller]")]
	[ApiController]
	public class ReviewerController : Controller
	{
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository,IMapper mapper)
        {
            _mapper = mapper;
            _reviewerRepository = reviewerRepository;  
        }

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
		public IActionResult GetReviewers()
		{
			var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(reviewers);
		}

		[HttpGet("{reviewerId}")]
		[ProducesResponseType(200, Type = typeof(Reviewer))]
		[ProducesResponseType(400)]
		public IActionResult GetReviewer(int reviewerId)
		{
			if (!_reviewerRepository.ExistReviewer(reviewerId))
				return NotFound();

			var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(reviewer);
		}

		[HttpGet("{reviewerId}/reviews")]
		public IActionResult GetReviewsByAReviewer(int reviewerId)
		{
			if (!_reviewerRepository.ExistReviewer(reviewerId))
				return NotFound();

			var reviews = _mapper.Map<List<ReviewDto>>(
				_reviewerRepository.GetReviewByReviewers(reviewerId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(reviews);
		}
	}
}
