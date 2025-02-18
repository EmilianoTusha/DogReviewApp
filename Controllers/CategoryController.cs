﻿using AutoMapper;
using DogReviewApp.Dto;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;
using DogReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DogReviewApp.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[Controller]")]
	[ApiController]
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
		public IActionResult GetCategories()
		{
			var category = _mapper.Map<List<Categorydto>>(_categoryRepository.GetCategories());
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(category);
		}
		[HttpGet("{categoryId}")]
		[ProducesResponseType(200, Type = typeof(Category))]
		[ProducesResponseType(400)]
		public IActionResult GetCategory(int categoryId)
		{
			if (!_categoryRepository.CategoryExist(categoryId))
				return NotFound();

			var category = _mapper.Map<Categorydto>(_categoryRepository.GetCategory(categoryId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(category);
		}
		[HttpGet("dog/{categoryId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]
		[ProducesResponseType(400)]
		public IActionResult GetDogByCategoryId(int categoryId)
		{
			var DOGS = _mapper.Map<List<Categorydto>>(_categoryRepository.GetDogByCategory(categoryId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(DOGS);
		}
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public IActionResult CreateCategory([FromBody] Categorydto categoryCreate)
		{
			if (categoryCreate == null)
				return BadRequest(ModelState);

			var category = _categoryRepository.GetCategories()
				.Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
				.FirstOrDefault();

			if (category != null)
			{
				ModelState.AddModelError("", "Category already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var categoryMap = _mapper.Map<Category>(categoryCreate);

			if (!_categoryRepository.CreateCategory(categoryMap))
			{
				ModelState.AddModelError("", "Something went wrong while savin");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}


	}
}
