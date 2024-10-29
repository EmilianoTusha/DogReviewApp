using DogReviewApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DogReviewApp.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<DogOwner> DogOwners { get; set; }
        public DbSet<DogCategory> DogCategories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DogCategory>()
                .HasKey(dc => new { dc.DogId, dc.CategoryId });
            modelBuilder.Entity<DogCategory>()
                .HasOne(d => d.Dog)
                .WithMany(dc => dc.DogCategories)
                .HasForeignKey(d => d.DogId);
			modelBuilder.Entity<DogCategory>()
				.HasOne(d => d.Category)
				.WithMany(dc => dc.DogCategories)
				.HasForeignKey(c => c.CategoryId);

			modelBuilder.Entity<DogOwner>()
				.HasKey(Do => new { Do.DogId, Do.OwnerId });
			modelBuilder.Entity<DogOwner>()
				.HasOne(D => D.Dog)
				.WithMany(Do => Do.DogOwners)
				.HasForeignKey(D => D.DogId);

			modelBuilder.Entity<DogOwner>()
				.HasOne(o => o.Owner)
				.WithMany(Do => Do.DogOwners)
				.HasForeignKey(o => o.OwnerId);
		}
	}	
}
