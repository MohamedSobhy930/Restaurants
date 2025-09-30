using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restraurants.Domain.Entities;
using Restraurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
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
            modelBuilder.Entity<User>()
                .HasMany(r => r.Restaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(d => d.OwnerId);
            modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant
            {
                Id = 1,
                Name = "Pasta Palace",
                Category = "Italian",
                Description = "Authentic Italian pasta dishes",
                ContactEmail = "info@pastapalace.com",
                PhoneNumber = "1234567890",
                HasDelivery = true,
                OwnerId = "ad2e7d38-5ded-4022-89cc-9705cfd6b6e8"
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
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "c7b013f0-5201-4317-abd8-c211f91b7330", 
                Name = Restraurants.Domain.Utilities.UserRoles.Admin,
                NormalizedName = Restraurants.Domain.Utilities.UserRoles.Admin.ToUpper()
            },
            new IdentityRole
            {
                Id = "a6132b21-186c-4357-b353-731300e2cac9",
                Name = Restraurants.Domain.Utilities.UserRoles.Owner,
                NormalizedName = Restraurants.Domain.Utilities.UserRoles.Owner.ToUpper()
            },
            new IdentityRole
            {
                Id = "e2e3e595-188e-4f40-8f6a-4b0c776a3b6e",
                Name = Restraurants.Domain.Utilities.UserRoles.User,
                NormalizedName = Restraurants.Domain.Utilities.UserRoles.User.ToUpper()
            }
        );
        }
    }
}
