using AutoMapper;
using DogReviewApp.Data;
using DogReviewApp.Dto;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;
using DogReviewApp.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[Controller]")]
    [ApiController]
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;
        public DogController(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]
        public IActionResult GetDogs()
        {
            var dogs =_mapper.Map<List<Dogdto>> (_dogRepository.GetDogs());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(dogs);
        }
        [HttpGet ("{dogid}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]
        public IActionResult GetDog(int dogid)
        {
            if (!_dogRepository.DogExists(dogid))
                return NotFound();
            var dog = _mapper.Map<Dogdto>(_dogRepository.GetDog(dogid));
            if(!ModelState.IsValid)            
                return BadRequest(ModelState);
            return Ok(dog);
           
        }
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateDog([FromQuery] int ownerId,[FromQuery] int catid, [FromBody] Dogdto DogCreate)
		{
			if (DogCreate == null)
				return BadRequest(ModelState);

			var dogs = _dogRepository.GetDogs()
				.Where(c => c.Name.Trim().ToUpper() == DogCreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();

			if (dogs != null)
			{
				ModelState.AddModelError("", "Owner already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var DogMap = _mapper.Map<Dog>(DogCreate);

            if (!_dogRepository.CreateDog(ownerId, catid, DogMap))
			{
				ModelState.AddModelError("", "Something went wrong while savin");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}

	}
}
