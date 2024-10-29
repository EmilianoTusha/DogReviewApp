using DogReviewApp.Data;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;

namespace DogReviewApp.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DataContext _context;

		public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExist(int id)
		{
			return _context.Categories.Any( c => c.Id == id );
		}

		public bool CreateCategory(Category category)
		{
			_context.Add(category);
			return Save();
		}

		public ICollection<Category> GetCategories()
		{
			return _context.Categories.OrderBy(c => c.Id).ToList();
		}

		public Category GetCategory(int id)
		{
			return _context.Categories.Where(c => c.Id == id).FirstOrDefault();

		}
		public ICollection<Dog> GetDogByCategory(int categoryid)
		{
			return _context.DogCategories.Where(e => e.CategoryId == categoryid).Select(c => c.Dog).ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
