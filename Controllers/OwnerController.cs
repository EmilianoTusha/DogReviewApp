﻿using AutoMapper;
using DogReviewApp.Dto;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;
using DogReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : Controller
	{
		private readonly IOwnerRepository _ownerRepository;
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public OwnerController(IOwnerRepository ownerRepository,
			ICountryRepository countryRepository,
			IMapper mapper)
		{
			_ownerRepository = ownerRepository;
			_countryRepository = countryRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
		public IActionResult GetOwners()
		{
			var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(owners);
		}

		[HttpGet("{ownerId}")]
		[ProducesResponseType(200, Type = typeof(Owner))]
		[ProducesResponseType(400)]
		public IActionResult GetOwner(int ownerId)
		{
			if (!_ownerRepository.OwnerExists(ownerId))
				return NotFound();

			var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(owner);
		}

		[HttpGet("{ownerId}/Dog")]
		[ProducesResponseType(200, Type = typeof(Owner))]
		[ProducesResponseType(400)]
		public IActionResult GetDogByOwner(int ownerId)
		{
			if (!_ownerRepository.OwnerExists(ownerId))
			{
				return NotFound();
			}

			var owner = _mapper.Map<List<Dogdto>>(
				_ownerRepository.GetDogByOwner(ownerId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(owner);
		}

		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
		{
			if (ownerCreate == null)
				return BadRequest(ModelState);

			var owners = _ownerRepository.GetOwners()
				.Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();

			if (owners != null)
			{
				ModelState.AddModelError("", "Owner already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ownerMap = _mapper.Map<Owner>(ownerCreate);

			ownerMap.Country = _countryRepository.GetCountry(countryId);

			if (!_ownerRepository.CreateOwner(ownerMap))
			{
				ModelState.AddModelError("", "Something went wrong while savin");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}
	}
}