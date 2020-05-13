using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Car_House.Models;
using Car_House.Models.ViewModels;

namespace Car_House.Data
{
    public class Car_HouseContext : DbContext
    {
        public Car_HouseContext (DbContextOptions<Car_HouseContext> options)
            : base(options)
        {

        }

        public DbSet<Car_House.Models.User> User { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
                .Property(b => b.ProfilePicture)
                .HasDefaultValue("Person.jpg");
            // This statement deletes child instances of Car entity on delete cascade
            modelBuilder.Entity<Car>()
                .HasMany<Image>(x => x.Images)
                .WithOne(s => s.Car)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
              .Property(b => b.DisplayImage)
              .HasDefaultValue("red.jpg");
            modelBuilder.Entity<Car>()
            .Property(b => b.Color)
            .HasDefaultValue("none");
        }
    

    }
}
