using DogReviewApp.Model;

namespace DogReviewApp.Interfaces
{
	public interface IDogRepository
	{
		ICollection<Dog> GetDogs();
		Dog GetDog(int id);
		Dog GetDog(string name);	
		bool DogExists(int dogid);
		bool CreateDog(int ownerId, int CategoryId, Dog dog);
		bool Save();

	}
}
