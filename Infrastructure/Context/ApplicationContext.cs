using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Seeding
            builder.Entity<Product>().HasData(
                          new Product
                          {
                              Id = 1,
                              Name = "Pizza",
                              Description = "Pizza",
                              Price = 50.00m,
                              IsAvailable = true,
                              ReadyTime = new TimeSpan(hours: 1, minutes: 30, seconds: 0),
                              PictureUrl = "img/ProductImages/2.jpeg",
                              CategoryId = 1
                          },
                          new Product
                          {
                              Id = 2,
                              Name = "Orange Juice",
                              Description = "Orange",
                              Price = 60.00m,
                              IsAvailable = true,
                              ReadyTime = new TimeSpan(hours: 1, minutes: 30, seconds: 0),
                              PictureUrl = "img/ProductImages/83ac9eca-ff89-4e7c-bf65-ee47866b3c69.jpg",
                              CategoryId = 2
                          }
                      );

            builder.Entity<Office>().HasData(
                        new Office
                        {
                            ID = 1,
                            Name = "Manager",
                            IsKitchen = false,
                            Location = "First Floor",
                        },
                        new Office
                        {
                            ID = 2,
                            Name = "Kitchen1",
                            IsKitchen = true,
                            Location = "First Floor",
                        }
                    );

           builder.Entity<Category>().HasData(
                        new Category
                        {
                            Id = 1,
                            Type = "Food"
                        },
                        new Category
                        {
                            Id = 2,
                            Type = "Drink"
                        }
                    );
            #endregion

            builder.Entity<OrderProducts>()
                .HasKey(os => new { os.OrderId, os.ProductId });

            builder.Entity<Shifts>().HasData(
                      new Shifts
                      {
                          ID = 1,
                          ShiftStartTime = new TimeSpan(hours: 12, minutes: 0, seconds: 0),
                          ShiftEndTime = new TimeSpan(hours: 6, minutes: 30, seconds: 0),
                      },
                      new Shifts
                      {
                          ID = 2,
                          ShiftStartTime = new TimeSpan(hours: 6, minutes: 30, seconds: 0),
                          ShiftEndTime = new TimeSpan(hours: 12, minutes: 30, seconds: 0),
                      }
                  );

            base.OnModelCreating(builder);
        }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }












    }
}
