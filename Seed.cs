using DogReviewApp.Data;
using DogReviewApp.Model;

namespace DogReviewApp
{
	public class Seed
	{
		private readonly DataContext dataContext;
		public Seed(DataContext context)
		{
			this.dataContext = context;
		}
		public void SeedDataContext()
		{
			if (!dataContext.DogOwners.Any())
			{
				var dogOwners = new List<DogOwner>()
			{
				new DogOwner()
				{
					Dog = new Dog()
					{
						Name = "Max",
						BirthDate = new DateTime(2018,5,10),
						DogCategories = new List<DogCategory>()
						{
							new DogCategory { Category = new Category() { Name = "Husky"}}
						},
						Reviews = new List<Review>()
						{
							new Review { title = "Max", text = "Max is a very energetic dog",
							Reviewer = new Reviewer() { FirstName = "John", LastName = "Doe" } },
							new Review { title = "Max", text = "Max is great at running",
							Reviewer = new Reviewer() { FirstName = "Jane", LastName = "Smith" } }
						}
					},
					Owner = new Owner()
					{
						Name = "Alice",
						Gym = "Downtown Dog Park",
						Country = new Country()
						{
							Name = "USA"
						}
					}
				},
				new DogOwner()
				{
					Dog = new Dog()
					{
						Name = "Bella",
						BirthDate = new DateTime(2016,3,15),
						DogCategories = new List<DogCategory>()
						{
							new DogCategory { Category = new Category() { Name = "Golden Retriever"}}
						},
						Reviews = new List<Review>()
						{
							new Review { title = "Bella", text = "Bella is very friendly",
							Reviewer = new Reviewer() { FirstName = "Chris", LastName = "Evans" } },
							new Review { title = "Bella", text = "Bella loves to swim",
							Reviewer = new Reviewer() { FirstName = "Emma", LastName = "Watson" } }
						}
					},
					Owner = new Owner()
					{
						Name = "Robert",
						Gym = "Central Dog Park",
						Country = new Country()
						{
							Name = "Canada"
						}
					}
				},
				new DogOwner()
				{
					Dog = new Dog()
					{
						Name = "Rocky",
						BirthDate = new DateTime(2020,1,5),
						DogCategories = new List<DogCategory>()
						{
							new DogCategory { Category = new Category() { Name = "Bulldog"}}
						},
						Reviews = new List<Review>()
						{
							new Review { title = "Rocky", text = "Rocky is a brave dog",
							Reviewer = new Reviewer() { FirstName = "Bruce", LastName = "Wayne" } },
							new Review { title = "Rocky", text = "Rocky is great at guarding",
							Reviewer = new Reviewer() { FirstName = "Clark", LastName = "Kent" } }
						}
					},
					Owner = new Owner()
					{
						Name = "Diana",
						Gym = "Westside Dog Park",
						Country = new Country()
						{
							Name = "UK"
						}
					}
				}
			};

				dataContext.DogOwners.AddRange(dogOwners);
				dataContext.SaveChanges();
			}
		}
	}
}
