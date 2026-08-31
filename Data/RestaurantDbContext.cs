using Microsoft.EntityFrameworkCore;
using RestaurantApplication.Models;

namespace RestaurantApplication.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Burger", DisplayOrder=2 },
                new Category { Id = 2, Name = "Pizza", DisplayOrder= 1 }
                );
            modelBuilder.Entity<Food>().HasData(
                new Food { Id = 1, Name = "Delicious Pizza", Description= "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",Price = 20.00m,CategoryId=2,ImageUrl= "/images/f2.png" },
                new Food { Id = 2, Name = "Delicious Burger", Description= "Veniam debitis quaerat officiis quasi cupiditate quo, quisquam velit, magnam voluptatem repellendus sed eaque",Price = 40.50m,CategoryId=1 ,ImageUrl = "/images/f3.png" }
                );

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Foods { get; set; }

    }
}
