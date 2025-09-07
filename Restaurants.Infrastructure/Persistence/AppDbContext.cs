using Microsoft.EntityFrameworkCore;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>()
                .OwnsOne(r => r.Address);
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Dishes)
                .WithOne()
                .HasForeignKey(d => d.RestaurantId);
            modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant
            {
                Id = 1,
                Name = "Pasta Palace",
                Category = "Italian",
                Description = "Authentic Italian pasta dishes",
                ContactEmail = "info@pastapalace.com",
                PhoneNumber = "1234567890",
                HasDelivery = true
            }
        );
            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address).HasData(
            new
            {
                RestaurantId = 1,
                City = "Rome",
                Street = "Lazio",
                PostalCode = "00100"
            }
        );
            modelBuilder.Entity<Dish>().HasData(
            new Dish
            {
                Id = 1,
                Name = "Spaghetti Carbonara",
                Description = "Classic Roman pasta with egg, cheese, pancetta, and pepper",
                Price = 12.50m,
                RestaurantId = 1
            },
            new Dish
            {
                Id = 2,
                Name = "Lasagna",
                Description = "Layers of pasta, meat sauce, and cheese baked to perfection",
                Price = 15.00m,
                RestaurantId = 1
            }
        );
        }
    }
}
