using DogReviewApp.Data;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;

namespace DogReviewApp.Repository
{
	public class DogRepository : IDogRepository
	{
        private readonly DataContext _context;
        public DogRepository(DataContext context)
        {
            _context = context;
        }

		public bool CreateDog(int ownerId, int CategoryId, Dog dog)
		{
			var dogOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
			var category = _context.Categories.Where(c => c.Id == CategoryId).FirstOrDefault();

			var dogOwner = new DogOwner()
			{
				Owner = dogOwnerEntity,
				Dog = dog,
			};
			_context.Add(dogOwner);
			
			var dogCategory = new DogCategory()
			{
				Category = category,
				Dog = dog,
			};
			_context.Add(dogCategory);

			_context.Add(dog);

			return Save();
		}

		public bool DogExists(int dogid)
		{
			return _context.Dogs.Any(p => p.Id == dogid);
		}

		public Dog GetDog(int id)
		{
			return _context.Dogs.Where(p => p.Id == id).FirstOrDefault();
		}

		public Dog GetDog(string name)
		{
			return _context.Dogs.Where(p => p.Name == name).FirstOrDefault();
		}	

		public ICollection<Dog> GetDogs() {
            return _context.Dogs.OrderBy(d => d.Id).ToList();
        
        }

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
