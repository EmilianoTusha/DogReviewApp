namespace DogReviewApp.Model
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; } 
		public ICollection<DogCategory> DogCategories { get; set; }

	}
}
