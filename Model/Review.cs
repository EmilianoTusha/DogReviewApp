namespace DogReviewApp.Model
{
	public class Review
	{
		public int Id { get; set; }
		public string title { get; set; }
		public string text { get; set; }
		public Reviewer Reviewer { get; set; }
		public Dog Dog { get; set; }


	}
}
