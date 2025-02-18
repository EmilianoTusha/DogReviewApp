﻿using DogReviewApp.Data;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DogReviewApp.Repository
{
	public class ReviewerRepository : IReviewerRepository
	{
        private readonly DataContext _context;
        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

		public bool ExistReviewer(int reviewerId)
		{
			return _context.Reviewers.Any(r => r.Id == reviewerId);
		}

		public ICollection<Review> GetReviewByReviewers(int reviewerId)
		{
			return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
		}

		public Reviewer GetReviewer(int reviewerId)
		{
			return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
		}

		public ICollection<Reviewer> GetReviewers()
		{
			return _context.Reviewers.ToList();
		}
	}
}
