using DogReviewApp.Model;

namespace DogReviewApp.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<Owner> GetOwners();
		Owner GetOwner(int ownerId);
		ICollection<Owner> GetOwnerOfADog(int DogId);
		ICollection<Dog> GetDogByOwner(int ownerId);
		bool OwnerExists(int ownerId);
		bool CreateOwner(Owner owner);
		bool Save();
	}
}
