using DogReviewApp.Data;
using DogReviewApp.Interfaces;
using DogReviewApp.Model;

namespace DogReviewApp.Repository
{
	public class OwnerRepository : IOwnerRepository
	{
		private readonly DataContext _context;

		public OwnerRepository(DataContext context)
		{
			_context = context;
		}

		public bool CreateOwner(Owner owner)
		{
			_context.Add(owner);
			return Save();
		}
		public Owner GetOwner(int ownerId)
		{
			return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
		}

		public ICollection<Owner> GetOwnerOfADog(int DogId)
		{
			return _context.DogOwners.Where(p => p.Dog.Id == DogId).Select(o => o.Owner).ToList();
		}

		public ICollection<Owner> GetOwners()
		{
			return _context.Owners.ToList();
		}

		public ICollection<Dog> GetDogByOwner(int ownerId)
		{
			return _context.DogOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Dog).ToList();
		}

		public bool OwnerExists(int ownerId)
		{
			return _context.Owners.Any(o => o.Id == ownerId);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
