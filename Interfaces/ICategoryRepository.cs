using DogReviewApp.Model;

namespace DogReviewApp.Interfaces
{
	public interface ICategoryRepository
	{
		ICollection<Category> GetCategories();
		Category GetCategory(int id);
		ICollection<Dog> GetDogByCategory(int categoryid);
		bool CategoryExist(int categoryid);
		bool CreateCategory(Category category);
		bool Save();
	}
}
