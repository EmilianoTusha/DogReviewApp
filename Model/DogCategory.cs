﻿using System.ComponentModel;

namespace DogReviewApp.Model
{
	public class DogCategory
	{
		public int DogId { get; set; }
		public int CategoryId { get; set; }
        public Dog Dog { get; set; }
        public Category Category { get; set; }

    }
}
 